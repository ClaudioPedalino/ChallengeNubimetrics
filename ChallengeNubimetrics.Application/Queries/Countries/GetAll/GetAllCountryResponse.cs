using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengeNubimetrics.Application.Queries.Countries.GetAll
{
    public class GetAllCountryResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Locale { get; set; }

        [JsonProperty("currency_id")]
        public string CurrencyId { get; set; }
    }
}
