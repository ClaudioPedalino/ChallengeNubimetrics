using ChallengeNubimetrics.Application.Models.Search;

namespace ChallengeNubimetrics.Application.Queries.Search
{
    public record GetBySearchResponse
    {
        public SearchDto Data { get; init; }
    }
}
