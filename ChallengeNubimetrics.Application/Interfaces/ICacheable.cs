using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengeNubimetrics.Application.Interfaces
{
    public interface ICacheable
    {
        string CacheKey { get; }
    }
}
