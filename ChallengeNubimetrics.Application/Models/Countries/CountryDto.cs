using Newtonsoft.Json;
using System.Collections.Generic;

namespace ChallengeNubimetrics.Application.Models.Countries
{
    public class CountryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Locale { get; set; }
        
        [JsonProperty("currency_id")]
        public string CurrencyId { get; set; }
        
        [JsonProperty("decimal_separator")]
        public string DecimalSeparator { get; set; }
        
        [JsonProperty("thousands_separator")]
        public string ThousandsSeparator { get; set; }

        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }

        [JsonProperty("geo_information")]
        public GeoInformationDto GeoInformation { get; set; }
        public List<StateDto> States { get; set; }
    }

    public class GeoInformationDto
    {
        public LocationDto Location { get; set; }
    }

    public class LocationDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public partial class StateDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
