using ChallengeNubimetrics.Application.Interfaces;
using ChallengeNubimetrics.Application.Models.Common;

namespace ChallengeNubimetrics.Application.Queries.Users.GetAll
{
    public record GetAllUserQuery : GetWithPagination
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
