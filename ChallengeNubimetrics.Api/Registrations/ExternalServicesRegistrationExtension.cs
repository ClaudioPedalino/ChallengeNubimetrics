using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChallengeNubimetrics.Api.Registrations
{
    public static class ExternalServicesRegistrationExtension
    {
        public static IServiceCollection AddExternalServices(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddHttpClient("MELI_CountriesServiceUrl",
                c => c.BaseAddress = new System.Uri(_configuration.GetValue<string>("Integrations:MELI_CountriesServiceUrl")));

            services.AddHttpClient("MELI_SearchServiceUrl",
                c => c.BaseAddress = new System.Uri(_configuration.GetValue<string>("Integrations:MELI_SearchServiceUrl")));

            services.AddHttpClient("MELI_ConversionServiceUrl",
                c => c.BaseAddress = new System.Uri(_configuration.GetValue<string>("Integrations:MELI_ConversionServiceUrl")));

            services.AddHttpClient("MELI_CurrenciesServiceUrl",
                c => c.BaseAddress = new System.Uri(_configuration.GetValue<string>("Integrations:MELI_CurrenciesServiceUrl")));

            return services;
        }
    }
}
