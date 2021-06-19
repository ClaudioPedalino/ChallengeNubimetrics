using Newtonsoft.Json;

namespace ChallengeNubimetrics.Application.Queries.Countries.GetAll
{
    public record GetAllCountryResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Locale { get; set; }

        [JsonProperty("currency_id")]
        public string CurrencyId { get; set; }
    }
}
