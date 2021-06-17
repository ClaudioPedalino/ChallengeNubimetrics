using ChallengeNubimetrics.Domain.Common;
using Microsoft.AspNetCore.Identity;
using System;

namespace ChallengeNubimetrics.Domain.Entities
{
    public class User : IdentityUser, IDeleteableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? DeleteAt { get; set; }
        public string DeleteBy { get; set; }
    }


}
