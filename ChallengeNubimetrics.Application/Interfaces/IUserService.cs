using ChallengeNubimetrics.Application.Auth;
using ChallengeNubimetrics.Application.Commands.Users;
using ChallengeNubimetrics.Application.Commands.Users.Delete;
using ChallengeNubimetrics.Application.Models.Common;
using ChallengeNubimetrics.Application.Queries.Users.GetAll;
using System.Threading.Tasks;

namespace ChallengeNubimetrics.Application.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Get all users from database
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PaginatedResult<GetAllUserResponse>> GetAllAsync(GetAllUserQuery request);

        /// <summary>
        /// Get login bearer token to access giving user name and password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AuthenticationResult> LoginAsync(CreateLoginUserCommand request);

        /// <summary>
        /// Create a new user into app
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AuthenticationResult> RegisterAsync(CreateRegisterUserCommand request);

        /// <summary>
        /// Delete user from app, internal set deleteable, dont remove registry from database
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Result> DeleteAsync(DeleteUserCommand request);
    }
}
