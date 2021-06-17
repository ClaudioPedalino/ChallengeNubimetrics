using System.ComponentModel.DataAnnotations;

namespace ChallengeNubimetrics.Application.Commands.Users
{
    public class CreateLoginUserCommand
    {
        public CreateLoginUserCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
