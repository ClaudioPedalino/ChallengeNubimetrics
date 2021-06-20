using ChallengeNubimetrics.Application.Extensions;
using ChallengeNubimetrics.Application.Queries.Countries.GetAll;
using MediatR;
using Newtonsoft.Json;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ChallengeNubimetrics.Application.Handlers.Countries.GetAll
{
    public class GetAllCountryQueryHandler : IRequestHandler<GetAllCountryQuery, IEnumerable<GetAllCountryResponse>>
    {
        private readonly IHttpClientFactory _httpFactory;
        private readonly ILogger _logger;
        /// private readonly AsyncRetryPolicy _retryPolicy;

        public GetAllCountryQueryHandler(IHttpClientFactory httpFactory, ILogger logger)
        {
            _httpFactory = httpFactory;
            _logger = logger;
            ///_retryPolicy = Policy.Handle<CustomApplicationException>()
            ///    .WaitAndRetryAsync(retryCount: 2,
            ///                       sleepDurationProvider: times => TimeSpan.FromMilliseconds(times * 100));
        }


        public async Task<IEnumerable<GetAllCountryResponse>> Handle(GetAllCountryQuery request, CancellationToken cancellationToken)
        {
            var client = _httpFactory.CreateClient("MELI_CountriesServiceUrl");

            using var serviceResult = await client.GetAsync(client.BaseAddress);

            if (!serviceResult.IsSuccessStatusCode)
                serviceResult.ExecuteMeliLogging(_logger, client.BaseAddress.ToString(), throwEx: true);


            var jsonResult = await serviceResult.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<List<GetAllCountryResponse>>(jsonResult);

            return response.OrderBy(x => x.Name);


            #region using polly handling scenario by scneario
            ///return await _retryPolicy.ExecuteAsync(async () =>
            ///{
            ///    using var serviceResult = await client.GetAsync(client.BaseAddress);

            ///    if (!serviceResult.IsSuccessStatusCode)
            ///    {
            ///        ExecuteLogging(client.BaseAddress.ToString(), serviceResult);
            ///        throw new CustomApplicationException($":(  Problems into => {client.BaseAddress}");
            ///    }

            ///    var jsonResult = await serviceResult.Content.ReadAsStringAsync();
            ///    var response = JsonConvert.DeserializeObject<List<GetAllCountryResponse>>(jsonResult);

            ///    return response.OrderBy(x => x.Name);
            ///});
            #endregion
        }
    }

}
