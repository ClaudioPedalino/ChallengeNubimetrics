using System;

namespace ChallengeNubimetrics.Domain.Common
{
    public interface ICreationableEntity
    {
        DateTime? CreatedAt { get; set; }
        string CreatedBy { get; set; }
    }
}