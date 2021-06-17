using ChallengeNubimetrics.Application.Auth;
using ChallengeNubimetrics.Application.Commands.Users;
using ChallengeNubimetrics.Application.Commands.Users.Delete;
using ChallengeNubimetrics.Application.Models.Common;
using ChallengeNubimetrics.Application.Queries.Users.GetAll;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeNubimetrics.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<GetAllUserResponse>> GetAllAsync();

        Task<AuthenticationResult> LoginAsync(CreateLoginUserCommand request);
        Task<AuthenticationResult> RegisterAsync(CreateRegisterUserCommand request);
        Task<Result> DeleteAsync(DeleteUserCommand request);
    }
}
