using Newtonsoft.Json;
using System.Collections.Generic;

namespace ChallengeNubimetrics.Application.Models.Countries
{
    public class CountryDto
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public string Locale { get; init; }

        [JsonProperty("currency_id")]
        public string CurrencyId { get; init; }

        [JsonProperty("decimal_separator")]
        public string DecimalSeparator { get; init; }

        [JsonProperty("thousands_separator")]
        public string ThousandsSeparator { get; init; }

        [JsonProperty("time_zone")]
        public string TimeZone { get; init; }

        [JsonProperty("geo_information")]
        public GeoInformationDto GeoInformation { get; init; }
        public List<StateDto> States { get; init; }
    }

    public class GeoInformationDto
    {
        public LocationDto Location { get; init; }
    }

    public class LocationDto
    {
        public double Latitude { get; init; }
        public double Longitude { get; init; }
    }

    public partial class StateDto
    {
        public string Id { get; init; }
        public string Name { get; init; }
    }
}
