using ChallengeNubimetrics.Application.Models.Countries;
using ChallengeNubimetrics.Application.Queries.Countries.GetById;
using ChallengeNubimetrics.Domain.Exceptions;
using MediatR;
using Newtonsoft.Json;
using Serilog;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ChallengeNubimetrics.Application.Handlers.Countries.GetById
{
    public class GetCountryByIdQueryHandler : IRequestHandler<GetCountryByIdQuery, GetCountryByIdResponse>
    {
        private readonly IHttpClientFactory _httpFactory;
        private readonly ILogger _logger;

        public GetCountryByIdQueryHandler(IHttpClientFactory httpFactory, ILogger logger)
        {
            _httpFactory = httpFactory;
            _logger = logger;
        }


        public async Task<GetCountryByIdResponse> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
        {
            var client = _httpFactory.CreateClient("MELI_CountriesServiceUrl");
            var url = $"{client.BaseAddress}/{request.CountryId.ToUpper()}";

            using var serviceResult = await client.GetAsync(url);

            if (!serviceResult.IsSuccessStatusCode)
            {
                ExecuteLogging(url, serviceResult);
                return new GetCountryByIdResponse();
            }

            var jsonResult = await serviceResult.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<CountryDto>(jsonResult);

            return new GetCountryByIdResponse() { Data = response };
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
