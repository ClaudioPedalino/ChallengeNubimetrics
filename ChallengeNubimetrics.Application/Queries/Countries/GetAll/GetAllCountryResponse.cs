using Newtonsoft.Json;

namespace ChallengeNubimetrics.Application.Queries.Countries.GetAll
{
    public record GetAllCountryResponse
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public string Locale { get; init; }

        [JsonProperty("currency_id")]
        public string CurrencyId { get; init; }
    }
}
