using System;

namespace ChallengeNubimetrics.Domain.Common
{
    public abstract class AuditableEntity : IDeleteableEntity, ICreationableEntity, IUpdateableEntity
    {
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? DeleteAt { get; set; }
        public string DeleteBy { get; set; }
    }
}