using Newtonsoft.Json;
using System.Diagnostics;

namespace ChallengeNubimetrics.Application.Queries.Currencies.GetAll
{
    [DebuggerDisplay("{Id} - {Description}")]
    public class GetAllCurrencyResponse
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string Description { get; set; }

        [JsonProperty("decimal_places")]
        public long DecimalPlaces { get; set; }

        [JsonProperty("to_dolar")]
        public decimal ToDolar { get; set; }

        public string Ratio { get; set; }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}
