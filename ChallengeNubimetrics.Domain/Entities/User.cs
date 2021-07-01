using ChallengeNubimetrics.Domain.Common;
using Microsoft.AspNetCore.Identity;
using System;

namespace ChallengeNubimetrics.Domain.Entities
{
    public class User : IdentityUser, IDeleteableEntity
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? DeleteAt { get; set; }
        public string DeleteBy { get; set; }
    }
}