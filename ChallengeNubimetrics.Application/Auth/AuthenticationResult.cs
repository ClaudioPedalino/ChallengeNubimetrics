using ChallengeNubimetrics.Application.Models.Common;
using System.Collections.Generic;

namespace ChallengeNubimetrics.Application.Auth
{
    public record AuthenticationResult : Result
    {
        public string Token { get; init; }
        public IEnumerable<string> ErrorMessages { get; init; }
    }
}
