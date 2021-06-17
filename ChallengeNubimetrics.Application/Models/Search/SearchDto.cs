using Newtonsoft.Json;
using System.Collections.Generic;

namespace ChallengeNubimetrics.Application.Models.Search
{
    public class SearchDto
    {
        [JsonProperty("site_id")]
        public string SiteId { get; set; }

        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }

        [JsonProperty("results")]
        public List<Result> Results { get; set; }

        [JsonProperty("secondary_results")]
        public List<object> SecondaryResults { get; set; } // TODO: CPedalino Siempre vienen vacios, por eso lo dejo 'object' :(

        [JsonProperty("related_results")]
        public List<object> RelatedResults { get; set; } // TODO: CPedalino Siempre vienen vacios, por eso lo dejo 'object' :(

        [JsonProperty("sort")]
        public Sort Sort { get; set; }

        [JsonProperty("available_sorts")]
        public List<Sort> AvailableSorts { get; set; }

        [JsonProperty("filters")]
        public List<Filter> Filters { get; set; }

        [JsonProperty("available_filters")]
        public List<AvailableFilter> AvailableFilters { get; set; }
    }

    public class AvailableFilter
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("values")]
        public List<AvailableFilterValue> Values { get; set; }
    }

    public class AvailableFilterValue
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("results")]
        public long Results { get; set; }
    }

    public class Sort
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Filter
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("values")]
        public List<FilterValue> Values { get; set; }
    }

    public class FilterValue
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("path_from_root")]
        public List<Sort> PathFromRoot { get; set; }
    }

    public class Paging
    {
        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("primary_results")]
        public long PrimaryResults { get; set; }

        [JsonProperty("offset")]
        public long Offset { get; set; }

        [JsonProperty("limit")]
        public long Limit { get; set; }
    }

    public class Result
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("site_id")]
        public string SiteId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("seller")]
        public Seller Seller { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("permalink")]
        public string Permalink { get; set; }
    }

    public class Seller
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("permalink")]
        public string Permalink { get; set; }

    }

}
