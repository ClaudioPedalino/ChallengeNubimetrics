using ChallengeNubimetrics.Application.Extensions;
using ChallengeNubimetrics.Application.Models.Search;
using ChallengeNubimetrics.Application.Queries.Search;
using MediatR;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ChallengeNubimetrics.Application.Handlers.Search
{
    public class GetBySearchQueryHandler : IRequestHandler<GetBySearchQuery, GetBySearchResponse>
    {
        private readonly IHttpClientFactory _httpFactory;
        private readonly ILogger _logger;

        public GetBySearchQueryHandler(IHttpClientFactory httpFactory, ILogger logger)
        {
            _httpFactory = httpFactory;
            _logger = logger;
        }

        public async Task<GetBySearchResponse> Handle(GetBySearchQuery request, CancellationToken cancellationToken)
        {
            var client = _httpFactory.CreateClient("MELI_SearchServiceUrl");
            string url = GetQueryString(request, client);

            using HttpResponseMessage serviceResult = await client.GetAsync(url);

            if (!serviceResult.IsSuccessStatusCode)
            {
                return new GetBySearchResponse();
            }

            var response = await serviceResult.ReadResponse<SearchDto>();

            return new GetBySearchResponse() { Data = response };
        }


        private static string GetQueryString(GetBySearchQuery request, HttpClient client)
        {
            var builder = new UriBuilder(client.BaseAddress) { Port = -1 };
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["q"] = request.Search;
            builder.Query = query.ToString();
            return builder.ToString();
        }
    }

}
