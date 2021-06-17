using System;

namespace ChallengeNubimetrics.Domain.Common
{
    public interface IUpdateableEntity
    {
        DateTime? UpdateAt { get; set; }
        string UpdateBy { get; set; }
    }
}