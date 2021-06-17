using System;

namespace ChallengeNubimetrics.Domain.Common
{
    public interface IDeleteableEntity
    {
        DateTime? DeleteAt { get; set; }
        string DeleteBy { get; set; }
    }
}