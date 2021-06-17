namespace ChallengeNubimetrics.Application.Models.Common
{
    public abstract class GetWithPagination
    {
        public GetWithPagination()
        {
            PageNumber = 1;
            PageSize = 10;
        }

        public virtual int PageNumber { get; set; }
        public virtual int PageSize { get; set; }
    }
}
