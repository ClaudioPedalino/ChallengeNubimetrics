using ChallengeNubimetrics.Application.Models.Common;
using System.Collections.Generic;

namespace ChallengeNubimetrics.Application.Auth
{
    public record AuthenticationResult : Result
    {
        public string Token { get; set; }
        public IEnumerable<string> ErrorMessages { get; set; }
    }
}
