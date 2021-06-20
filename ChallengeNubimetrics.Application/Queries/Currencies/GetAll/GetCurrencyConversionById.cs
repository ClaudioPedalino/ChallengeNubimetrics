using Newtonsoft.Json;
using System;

namespace ChallengeNubimetrics.Application.Queries.Currencies.GetAll
{
    public record GetCurrencyConversionById
    {
        [JsonProperty("currency_base")]
        public string CurrencyBase { get; init; }

        [JsonProperty("currency_quote")]
        public string CurrencyQuote { get; init; }

        public double Ratio { get; init; }

        public double Rate { get; init; }

        [JsonProperty("inv_rate")]
        public double InvRate { get; init; }

        [JsonProperty("creation_date")]
        public DateTimeOffset CreationDate { get; init; }

        [JsonProperty("valid_until")]
        public DateTimeOffset ValidUntil { get; init; }
    }
}
