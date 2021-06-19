using ChallengeNubimetrics.Application.Interfaces;
using MediatR;

namespace ChallengeNubimetrics.Application.Queries.Search
{
    public record GetBySearchQuery : IRequest<GetBySearchResponse>, ICacheable
    {
        public GetBySearchQuery(string search)
        {
            Search = search;
        }

        public string Search { get; set; }

        public string CacheKey => $"{GetType().Name}-{Search}";
    }

}
