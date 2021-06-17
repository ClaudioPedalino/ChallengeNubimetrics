using ChallengeNubimetrics.Application.Models.Common;

namespace ChallengeNubimetrics.Application.Queries.Users.GetAll
{
    public class GetAllUsersQuery : GetWithPagination
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
