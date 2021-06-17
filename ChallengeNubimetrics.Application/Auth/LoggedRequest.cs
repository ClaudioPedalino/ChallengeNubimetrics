using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengeNubimetrics.Application.Auth
{
    public class LoggedRequest
    {
        private string _userId;

        public string GetUser() => _userId;
        public void SetUser(string userId) => _userId = userId;
    }
}
