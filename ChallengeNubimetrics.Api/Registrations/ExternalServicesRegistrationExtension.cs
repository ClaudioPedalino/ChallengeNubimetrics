using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;

namespace ChallengeNubimetrics.Api.Registrations
{
    public static class ExternalServicesRegistrationExtension
    {
        public static IServiceCollection AddExternalServices(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddHttpClient("MELI_CountriesServiceUrl",
                c => c.BaseAddress = new System.Uri(_configuration.GetValue<string>("Integrations:MELI_CountriesServiceUrl")))
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(GetRetryPolicy(_configuration));

            services.AddHttpClient("MELI_SearchServiceUrl",
                c => c.BaseAddress = new System.Uri(_configuration.GetValue<string>("Integrations:MELI_SearchServiceUrl")))
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(GetRetryPolicy(_configuration));

            services.AddHttpClient("MELI_ConversionServiceUrl",
                c => c.BaseAddress = new System.Uri(_configuration.GetValue<string>("Integrations:MELI_ConversionServiceUrl")))
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(GetRetryPolicy(_configuration));

            services.AddHttpClient("MELI_CurrenciesServiceUrl",
                c => c.BaseAddress = new System.Uri(_configuration.GetValue<string>("Integrations:MELI_CurrenciesServiceUrl")))
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(GetRetryPolicy(_configuration));

            return services;


        }


        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(IConfiguration _configuration)
            => HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(retryCount: _configuration.GetValue<int>("ResilienceConfig:Retries"),
                               sleepDurationProvider: times => TimeSpan.FromMilliseconds(times * _configuration.GetValue<int>("ResilienceConfig:RetryDelayInMiliseconds")));
    }
}
