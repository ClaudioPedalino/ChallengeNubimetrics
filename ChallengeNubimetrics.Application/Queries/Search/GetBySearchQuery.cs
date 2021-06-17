using MediatR;

namespace ChallengeNubimetrics.Application.Queries.Search
{
    public class GetBySearchQuery : IRequest<GetBySearchResponse>
    {
        public GetBySearchQuery(string search)
        {
            Search = search;
        }

        public string Search { get; set; }
    }
}
