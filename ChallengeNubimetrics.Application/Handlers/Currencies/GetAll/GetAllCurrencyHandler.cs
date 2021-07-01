using ChallengeNubimetrics.Application.Extensions;
using ChallengeNubimetrics.Application.Helpers;
using ChallengeNubimetrics.Application.Helpers.Enum;
using ChallengeNubimetrics.Application.Queries.Currencies.GetAll;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ChallengeNubimetrics.Application.Handlers.Currencies.GetAll
{
    public class GetAllCurrencyHandler : IRequestHandler<GetAllCurrencyQuery, IEnumerable<GetAllCurrencyResponse>>
    {
        private readonly IHttpClientFactory _httpFactory;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public GetAllCurrencyHandler(IHttpClientFactory httpFactory, ILogger logger, IConfiguration configuration)
        {
            _httpFactory = httpFactory;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IEnumerable<GetAllCurrencyResponse>> Handle(GetAllCurrencyQuery request, CancellationToken cancellationToken)
        {
            var currencyServiceClient = _httpFactory.CreateClient("MELI_CurrenciesServiceUrl");
            var resultCurrencies = await GetCurrenciesFromMeliService(currencyServiceClient);

            var conversionServiceClient = _httpFactory.CreateClient("MELI_ConversionServiceUrl");
            await SetDolarConversion(resultCurrencies, conversionServiceClient);
            SaveJsonFileResult(resultCurrencies, _configuration);
            File.WriteAllText(GetCSVPath(), string.Join(",", resultCurrencies.Select(x => x.Ratio)));

            return resultCurrencies;
        }

        private void SaveJsonFileResult(List<GetAllCurrencyResponse> resultCurrencies, IConfiguration configuration)
        {
            var filePath = Directory.GetCurrentDirectory()
                         + configuration.GetSection("LoggingFile:JsonFilePath").Value
                         + DateTimeHelper.GetSystemDate().Date.ToString("dd-MM-yyyy")
                         + FileFormatDictionary.FileFormats[FileFormat.JSON];
            string jsonData = JsonConvert.SerializeObject(resultCurrencies, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
        }

        private async Task SetDolarConversion(List<GetAllCurrencyResponse> resultCurrencies, HttpClient conversionServiceClient)
        {
            // Average response time => 290 milisecs  ;)
            //Parallel.ForEach(resultCurrencies, async currency =>
            //{
            //    string url = BuildQueryString(currency.Id, conversionServiceClient);
            //    using var conversionServiceResult = await conversionServiceClient.GetAsync(url);

            //    if (!conversionServiceResult.IsSuccessStatusCode)
            //    {
            //        ExecuteLogging(url, conversionServiceResult);
            //    }

            //    var jsonResult = await conversionServiceResult.Content.ReadAsStringAsync();
            //    var conversionResult = JsonConvert.DeserializeObject<GetCurrencyConversionById>(jsonResult);

            //    currency.ToDolar = conversionResult.InvRate != default ? Convert.ToDecimal(conversionResult.InvRate) : default;
            //    currency.Ratio = conversionResult.Ratio != default ? conversionResult.Ratio.ToString() : default;
            //});

            foreach (var currency in resultCurrencies)
            {
                string url = BuildQueryString(currency.Id, conversionServiceClient);
                using var conversionServiceResult = await conversionServiceClient.GetAsync(url);

                if (!conversionServiceResult.IsSuccessStatusCode)
                    conversionServiceResult.ExecuteMeliLogging(_logger, url);

                var jsonResult = await conversionServiceResult.Content.ReadAsStringAsync();
                var conversionResult = JsonConvert.DeserializeObject<GetCurrencyConversionById>(jsonResult);

                currency.UpdateDolarPrice(conversionResult.InvRate, conversionResult.Ratio.ToString());
            }
        }

        private string GetCSVPath() =>
            Directory.GetCurrentDirectory()
            + _configuration.GetSection("LoggingFile:CsvFilePath").Value
            + FileFormatDictionary.FileFormats[FileFormat.CSV];

        private async Task<List<GetAllCurrencyResponse>> GetCurrenciesFromMeliService(HttpClient currencyServiceClient)
        {
            using var currencyServiceResult = await currencyServiceClient.GetAsync(currencyServiceClient.BaseAddress);
            if (!currencyServiceResult.IsSuccessStatusCode)
            {
                currencyServiceResult.ExecuteMeliLogging(_logger, currencyServiceClient.BaseAddress.ToString());
                return new List<GetAllCurrencyResponse>();
            }

            var jsonResult = await currencyServiceResult.Content.ReadAsStringAsync();
            var currenciesResponse = JsonConvert.DeserializeObject<List<GetAllCurrencyResponse>>(jsonResult);
            return currenciesResponse;
        }

        private string BuildQueryString(string currencyIdFrom, HttpClient client, string currencyIdTo = "USD")
        {
            var builder = new UriBuilder(client.BaseAddress) { Port = -1 };
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["from"] = currencyIdFrom;
            query["to"] = currencyIdTo;
            builder.Query = query.ToString();
            return builder.ToString();
        }
    }
}