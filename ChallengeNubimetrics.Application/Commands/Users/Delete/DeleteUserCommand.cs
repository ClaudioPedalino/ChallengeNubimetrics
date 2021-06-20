using ChallengeNubimetrics.Application.Auth;
using System.ComponentModel.DataAnnotations;

namespace ChallengeNubimetrics.Application.Commands.Users.Delete
{
    public class DeleteUserCommand : LoggedRequest
    {
        public DeleteUserCommand(string email)
        {
            Email = email;
        }

        [EmailAddress]
        public string Email { get; init; }
    }
}
