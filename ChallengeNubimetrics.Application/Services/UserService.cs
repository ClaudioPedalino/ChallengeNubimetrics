using AutoMapper;
using ChallengeNubimetrics.Application.Auth;
using ChallengeNubimetrics.Application.Commands.Users;
using ChallengeNubimetrics.Application.Commands.Users.Delete;
using ChallengeNubimetrics.Application.Extensions;
using ChallengeNubimetrics.Application.Helpers;
using ChallengeNubimetrics.Application.Interfaces;
using ChallengeNubimetrics.Application.Models.Common;
using ChallengeNubimetrics.Application.Queries.Users.GetAll;
using ChallengeNubimetrics.Domain.Entities;
using ChallengeNubimetrics.Domain.Exceptions;
using ChallengeNubimetrics.Infraestructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChallengeNubimetrics.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IUserStore<User> _userStore;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager,
                           JwtSettings jwtSettings,
                           IConfiguration configuration,
                           IUserRepository userRepository,
                           IUserStore<User> userStore,
                           IMapper mapper)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _configuration = configuration;
            _userRepository = userRepository;
            _userStore = userStore;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<GetAllUserResponse>> GetAllAsync(GetAllUserQuery request)
        {
            var users = _userRepository.GetAll();

            var total = await users.CountAsync();

            users = users
                .WhereIf(!string.IsNullOrWhiteSpace(request.FirstName), x => x.FirstName.Contains(request.FirstName))
                .WhereIf(!string.IsNullOrWhiteSpace(request.LastName), x => x.LastName.Contains(request.LastName))
                .WhereIf(!string.IsNullOrWhiteSpace(request.Email), x => x.Email.Contains(request.Email));

            var paginatedResult = await users
                .Skip((request.PageNumber - 1) * request.PageSize)
                .OrderBy(x => x.Email)
                .Take(request.PageSize)
                .ToListAsync();

            var response = _mapper.Map<List<GetAllUserResponse>>(paginatedResult);

            return PaginatedResult<GetAllUserResponse>
                .Success(
                    totalCount: total,
                    pageSize: request.PageSize,
                    totalPages: (total / request.PageSize) + 1,
                    data: response);
        }

        public async Task<AuthenticationResult> LoginAsync(CreateLoginUserCommand request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return ValidateUserException(AuthValidationErrorResponses.UserDoesNotExist);

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!userHasValidPassword)
                return ValidateUserException(AuthValidationErrorResponses.UserOrPasswordAreIncorrect);

            return GenerateAuthResult(user);
        }

        public async Task<AuthenticationResult> RegisterAsync(CreateRegisterUserCommand request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                return ValidateUserException(AuthValidationErrorResponses.UserAlreadyExist);

            var newUser = _mapper.Map<User>(request);

            var createdUser = await _userManager.CreateAsync(newUser, request.Password);

            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    ErrorMessages = createdUser.Errors.Select(x => x.Description)
                };
            }

            return GenerateAuthResult(newUser);
        }

        public async Task<Result> DeleteAsync(DeleteUserCommand request)
        {
            var requester = await _userStore.FindByIdAsync(request.GetUser(), new CancellationToken());
            if (requester == null || requester.DeleteAt != null)
                return Result.Error(new UserDoesNotExistException().Message);

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return ValidateUserException(AuthValidationErrorResponses.UserDoesNotExist);

            /// Con ésto hariamos borrado físico, pero mejor hagamos baja lógica
            /// var result = await _userManager.DeleteAsync(user);
            /// if (!result.Succeeded)
            /// {
            ///     return new AuthenticationResult
            ///     {
            ///         ErrorMessages = result.Errors.Select(x => x.Description)
            ///     };
            /// }

            user.DeleteAt = DateTimeHelper.GetSystemDate();
            user.DeleteBy = requester.UserName;
            await _userRepository.UpdateAsync(user);

            return Result.Success($"User {user.UserName} deleted succesfully");
        }

        private AuthenticationResult GenerateAuthResult(IdentityUser newUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, newUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.AuthTime, DateTime.Now.ToString("d")),
                    new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
                    new Claim("id", newUser.Id),
                    new Claim("createdAt", DateTimeHelper.GetSystemDate().ToString()),
                }),
                Expires = DateTimeHelper.GetSystemDate().AddHours(Convert.ToInt32(_configuration.GetSection("JwtSettings:ValidHours").Value)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                                            SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationResult
            {
                HasErrors = false,
                Token = tokenHandler.WriteToken(token)
            };
        }

        private AuthenticationResult ValidateUserException(string validationMessage)
            => new AuthenticationResult
            {
                ErrorMessages = new[] { validationMessage }
            };
    }
}