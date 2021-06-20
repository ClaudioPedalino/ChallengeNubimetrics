using ChallengeNubimetrics.Application.Models.Countries;

namespace ChallengeNubimetrics.Application.Queries.Countries.GetById
{
    public record GetCountryByIdResponse
    {
        public CountryDto Data { get; init; }
    }
}
