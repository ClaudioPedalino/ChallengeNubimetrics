using ChallengeNubimetrics.Application.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengeNubimetrics.Application.Auth
{
    public class AuthenticationResult : Result
    {
        public string Token { get; set; }
        public IEnumerable<string> ErrorMessages { get; set; }
    }
}
