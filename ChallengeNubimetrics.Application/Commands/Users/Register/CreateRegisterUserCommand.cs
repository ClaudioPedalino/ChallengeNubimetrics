using System.ComponentModel.DataAnnotations;

namespace ChallengeNubimetrics.Application.Commands.Users.Register
{
    public class CreateRegisterUserCommand
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        [EmailAddress] public string Email { get; init; }
        public string Password { get; init; }
    }
}