using ChallengeNubimetrics.Application.Models.Common;

namespace ChallengeNubimetrics.Application.Queries.Users.GetAll
{
    public record GetAllUserQuery : GetWithPagination
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
    }
}
