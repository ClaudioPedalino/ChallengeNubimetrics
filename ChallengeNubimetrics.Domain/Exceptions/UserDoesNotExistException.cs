using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengeNubimetrics.Domain.Exceptions
{
    public class UserDoesNotExistException : CustomApplicationException
    {
        public UserDoesNotExistException()
            : base("User does not exist") { }
    }
}
