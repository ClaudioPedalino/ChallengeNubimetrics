﻿using Newtonsoft.Json;
using System;

namespace ChallengeNubimetrics.Application.Queries.Currencies.GetAll
{
    public record GetCurrencyConversionById
    {
        [JsonProperty("currency_base")]
        public string CurrencyBase { get; set; }

        [JsonProperty("currency_quote")]
        public string CurrencyQuote { get; set; }

        public double Ratio { get; set; }

        public double Rate { get; set; }

        [JsonProperty("inv_rate")]
        public double InvRate { get; set; }

        [JsonProperty("creation_date")]
        public DateTimeOffset CreationDate { get; set; }

        [JsonProperty("valid_until")]
        public DateTimeOffset ValidUntil { get; set; }
    }
}
