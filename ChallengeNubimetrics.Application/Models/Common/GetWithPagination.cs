namespace ChallengeNubimetrics.Application.Models.Common
{
    public abstract record GetWithPagination
    {
        public GetWithPagination()
        {
            PageNumber = 1;
            PageSize = 10;
        }

        public virtual int PageNumber { get; init; }
        public virtual int PageSize { get; init; }
    }
}