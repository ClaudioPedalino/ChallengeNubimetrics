using ChallengeNubimetrics.Application.Interfaces;
using MediatR;

namespace ChallengeNubimetrics.Application.Queries.Countries.GetById
{
    public record GetCountryByIdQuery : IRequest<GetCountryByIdResponse>, ICacheable
    {
        public GetCountryByIdQuery(string countryId)
        {
            CountryId = countryId;
        }

        /// <summary>
        /// Iso2 Id Format
        /// </summary>
        public string CountryId { get; init; }

        public string CacheKey => $"{GetType().Name}-{CountryId}";
    }
}