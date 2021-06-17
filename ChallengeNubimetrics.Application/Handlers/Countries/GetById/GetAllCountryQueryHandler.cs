using ChallengeNubimetrics.Application.Models.Countries;
using ChallengeNubimetrics.Application.Queries.Countries.GetAll;
using ChallengeNubimetrics.Domain.Exceptions;
using MediatR;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChallengeNubimetrics.Application.Handlers.Countries.GetById
{
    public class GetAllCountryQueryHandler : IRequestHandler<GetAllCountryQuery, IEnumerable<GetAllCountryResponse>>
    {
        private readonly IHttpClientFactory _httpFactory;
        private readonly ILogger _logger;

        public GetAllCountryQueryHandler(IHttpClientFactory httpFactory, ILogger logger)
        {
            _httpFactory = httpFactory;
            _logger = logger;
        }


        public async Task<IEnumerable<GetAllCountryResponse>> Handle(GetAllCountryQuery request, CancellationToken cancellationToken)
        {
            var client = _httpFactory.CreateClient("MELI_CountriesServiceUrl");

            using var serviceResult = await client.GetAsync(client.BaseAddress);

            if (!serviceResult.IsSuccessStatusCode)
            {
                ExecuteLogging(client.BaseAddress.ToString(), serviceResult);
                return new List<GetAllCountryResponse>();
            }

            var jsonResult = await serviceResult.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<List<GetAllCountryResponse>>(jsonResult);

            return response.OrderBy(x => x.Name);
        }

        private void ExecuteLogging(string url, HttpResponseMessage serviceResult)
        {
            if (serviceResult.StatusCode == HttpStatusCode.NotFound)
                _logger.Information($"[{serviceResult.StatusCode}] {new MeliServiceException(url).Message}");
            else
                _logger.Error($"[{serviceResult.StatusCode}] {new MeliServiceException(url).Message}");
        }
    }
}
