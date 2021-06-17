using ChallengeNubimetrics.Application.Auth;
using System.ComponentModel.DataAnnotations;

namespace ChallengeNubimetrics.Application.Commands.Users
{
    public class CreateRegisterUserCommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress] public string Email { get; set; }
        public string Password { get; set; }
    }
}
