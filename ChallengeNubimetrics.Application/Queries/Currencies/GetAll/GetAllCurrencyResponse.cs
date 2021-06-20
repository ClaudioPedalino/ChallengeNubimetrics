using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace ChallengeNubimetrics.Application.Queries.Currencies.GetAll
{
    [DebuggerDisplay("{Id} - {Description}")]
    public record GetAllCurrencyResponse
    {
        public string Id { get; init; }
        public string Symbol { get; init; }
        public string Description { get; init; }

        [JsonProperty("decimal_places")]
        public long DecimalPlaces { get; init; }

        [JsonProperty("to_dolar")]
        public decimal ToDolar { get; private set; }

        public string Ratio { get; private set; }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }

        public void UpdateDolarPrice(double toDolar, string ratio)
        {
            ToDolar = toDolar != default ? Convert.ToDecimal(toDolar) : default;
            Ratio = ratio != default ? ratio : default;
        }
    }
}
