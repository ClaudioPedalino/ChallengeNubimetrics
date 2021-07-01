using System.ComponentModel.DataAnnotations;

namespace ChallengeNubimetrics.Application.Commands.Users.Login
{
    public class CreateLoginUserCommand
    {
        public CreateLoginUserCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        [EmailAddress]
        public string Email { get; init; }

        public string Password { get; init; }
    }
}