using Newtonsoft.Json;
using System.Collections.Generic;

namespace ChallengeNubimetrics.Application.Models.Search
{
    public class SearchDto
    {
        [JsonProperty("site_id")]
        public string SiteId { get; init; }

        [JsonProperty("query")]
        public string Query { get; init; }

        [JsonProperty("paging")]
        public Paging Paging { get; init; }

        [JsonProperty("results")]
        public List<Result> Results { get; init; }

        [JsonProperty("secondary_results")]
        public List<object> SecondaryResults { get; init; } // TODO: CPedalino Siempre vienen vacios, por eso lo dejo 'object' :(

        [JsonProperty("related_results")]
        public List<object> RelatedResults { get; init; } // TODO: CPedalino Siempre vienen vacios, por eso lo dejo 'object' :(

        [JsonProperty("sort")]
        public Sort Sort { get; init; }

        [JsonProperty("available_sorts")]
        public List<Sort> AvailableSorts { get; init; }

        [JsonProperty("filters")]
        public List<Filter> Filters { get; init; }

        [JsonProperty("available_filters")]
        public List<AvailableFilter> AvailableFilters { get; init; }
    }

    public class AvailableFilter
    {
        [JsonProperty("id")]
        public string Id { get; init; }

        [JsonProperty("name")]
        public string Name { get; init; }

        [JsonProperty("type")]
        public string Type { get; init; }

        [JsonProperty("values")]
        public List<AvailableFilterValue> Values { get; init; }
    }

    public class AvailableFilterValue
    {
        [JsonProperty("id")]
        public string Id { get; init; }

        [JsonProperty("name")]
        public string Name { get; init; }

        [JsonProperty("results")]
        public long Results { get; init; }
    }

    public class Sort
    {
        [JsonProperty("id")]
        public string Id { get; init; }

        [JsonProperty("name")]
        public string Name { get; init; }
    }

    public class Filter
    {
        [JsonProperty("id")]
        public string Id { get; init; }

        [JsonProperty("name")]
        public string Name { get; init; }

        [JsonProperty("type")]
        public string Type { get; init; }

        [JsonProperty("values")]
        public List<FilterValue> Values { get; init; }
    }

    public class FilterValue
    {
        [JsonProperty("id")]
        public string Id { get; init; }

        [JsonProperty("name")]
        public string Name { get; init; }

        [JsonProperty("path_from_root")]
        public List<Sort> PathFromRoot { get; init; }
    }

    public class Paging
    {
        [JsonProperty("total")]
        public long Total { get; init; }

        [JsonProperty("primary_results")]
        public long PrimaryResults { get; init; }

        [JsonProperty("offset")]
        public long Offset { get; init; }

        [JsonProperty("limit")]
        public long Limit { get; init; }
    }

    public class Result
    {
        [JsonProperty("id")]
        public string Id { get; init; }

        [JsonProperty("site_id")]
        public string SiteId { get; init; }

        [JsonProperty("title")]
        public string Title { get; init; }

        [JsonProperty("seller")]
        public Seller Seller { get; init; }

        [JsonProperty("price")]
        public double Price { get; init; }

        [JsonProperty("permalink")]
        public string Permalink { get; init; }
    }

    public class Seller
    {
        [JsonProperty("id")]
        public long Id { get; init; }

        [JsonProperty("permalink")]
        public string Permalink { get; init; }
    }
}