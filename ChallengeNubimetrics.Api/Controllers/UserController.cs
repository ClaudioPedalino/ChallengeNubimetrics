using ChallengeNubimetrics.Application.Auth;
using ChallengeNubimetrics.Application.Commands.Users;
using ChallengeNubimetrics.Application.Commands.Users.Delete;
using ChallengeNubimetrics.Application.Extensions;
using ChallengeNubimetrics.Application.Interfaces;
using ChallengeNubimetrics.Application.Models.Common;
using ChallengeNubimetrics.Application.Queries.Users.GetAll;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChallengeNubimetrics.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/usuarios")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }



        [HttpGet("users")]
        [ProducesResponseType(typeof(PaginatedResult<GetAllUserResponse>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(UnauthorizedObjectResult), 401)]
        [ProducesResponseType(typeof(Result), 500)]
        [Authorize]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUserQuery request)
        {
            return Ok(await _userService.GetAllAsync(request));
        }


        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthenticationResult), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(UnauthorizedObjectResult), 401)]
        [ProducesResponseType(typeof(Result), 500)]
        [AllowAnonymous]
        public async Task<IActionResult> CreateLoginUser([FromBody] CreateLoginUserCommand request)
        {
            var response = await _userService.LoginAsync(request);
            if (response.HasErrors)
                return BadRequest(response.Message);

            return Ok(response);
        }


        [HttpPost("register")]
        [ProducesResponseType(typeof(AuthenticationResult), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(UnauthorizedObjectResult), 401)]
        [ProducesResponseType(typeof(Result), 500)]
        [AllowAnonymous]
        public async Task<IActionResult> CreateRegisterUser([FromBody] CreateRegisterUserCommand request)
        {
            var response = await _userService.RegisterAsync(request);
            if (response.HasErrors)
                return BadRequest(response.Message);

            return Ok(response);
        }


        [HttpDelete("delete")]
        [ProducesResponseType(typeof(Result), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(UnauthorizedObjectResult), 401)]
        [ProducesResponseType(typeof(Result), 500)]
        [Authorize]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserCommand request)
        {
            request.SetUser(User.GetUserId());
            var response = await _userService.DeleteAsync(request);
            if (response.HasErrors)
                return BadRequest(response.Message);

            return Ok(response);
        }
    }
}
