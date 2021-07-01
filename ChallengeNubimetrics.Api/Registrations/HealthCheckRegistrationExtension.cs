using ChallengeNubimetrics.Application.Helpers;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeNubimetrics.Api.Registrations
{
    public static class HealthCheckRegistrationExtension
    {
        public static IServiceCollection AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddSqlite(configuration.GetConnectionString("LiteDb"),
                    healthQuery: "SELECT 1;",
                    failureStatus: HealthStatus.Unhealthy,
                    tags: new[] { "ready" })
                .AddUrlGroup(new Uri(configuration.GetValue<string>("Integrations:MELI_CountriesServiceUrl")),
                    "MELI Countries Service",
                    HealthStatus.Degraded,
                    timeout: new TimeSpan(0, 0, 5),
                    tags: new[] { "ready" })
                .AddUrlGroup(new Uri(configuration.GetValue<string>("Integrations:MELI_SearchServiceUrl")),
                    "MELI Search Service",
                    HealthStatus.Degraded,
                    timeout: new TimeSpan(0, 0, 5),
                    tags: new[] { "ready" })
                .AddUrlGroup(new Uri(configuration.GetValue<string>("Integrations:MELI_CurrenciesServiceUrl")),
                    "MELI Currencies Service",
                    HealthStatus.Degraded,
                    timeout: new TimeSpan(0, 0, 5),
                    tags: new[] { "ready" })
                .AddUrlGroup(new Uri(configuration.GetValue<string>("Integrations:MELI_ConversionServiceUrl") + "?from=ars&to=USD"),
                    "MELI Conversion Service",
                    HealthStatus.Degraded,
                    timeout: new TimeSpan(0, 0, 5),
                    tags: new[] { "ready" })
                .AddCheck("File Path Health Check", new FilePathWriteHealthCheck(configuration["DummyFilePath"]),
                    HealthStatus.Unhealthy,
                    tags: new[] { "ready" });

            services.AddHealthChecksUI().AddInMemoryStorage();

            return services;
        }

        public static IApplicationBuilder UseHealhtChecks(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("api/health/ready", new HealthCheckOptions()
                {
                    ResultStatusCodes =
                    {
                            [HealthStatus.Healthy] = StatusCodes.Status200OK,
                            [HealthStatus.Degraded] = StatusCodes.Status500InternalServerError,
                            [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                    },
                    ResponseWriter = WriteHealthCheckReadyResponse,
                    Predicate = (check) => check.Tags.Contains("ready"),
                    AllowCachingResponses = false
                });
                endpoints.MapHealthChecks("api/health/live", new HealthCheckOptions()
                {
                    Predicate = (check) => !check.Tags.Contains("ready"),
                    ResponseWriter = WriteHealthCheckLiveResponse,
                    AllowCachingResponses = false
                });
                endpoints.MapHealthChecks("api/health/ui", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                //endpoints.MapHealthChecksUI();
            });

            app.UseHealthChecksUI();

            return app;
        }

        ///Enrich Health Check Detail
        private static Task WriteHealthCheckLiveResponse(HttpContext httpContext, HealthReport result)
        {
            httpContext.Response.ContentType = "application/json";

            var json = new JObject(
                    new JProperty("OverallStatus", result.Status.ToString()),
                    new JProperty("TotalCheckDuration", result.TotalDuration.TotalSeconds.ToString("0:0.00"))
                );
            return httpContext.Response.WriteAsync(json.ToString(Formatting.Indented));
        }

        private static Task WriteHealthCheckReadyResponse(HttpContext httpContext, HealthReport result)
        {
            httpContext.Response.ContentType = "application/json";

            var json = new JObject(
                    new JProperty("OverallStatus", result.Status.ToString()),
                    new JProperty("TotalCheckDuration", result.TotalDuration.TotalSeconds.ToString("0:0.00")),
                    new JProperty("DependencyHealthChecks", new JObject(result.Entries.Select(dicItem =>
                        new JProperty(dicItem.Key, new JObject(
                            new JProperty("Status", dicItem.Value.Status.ToString()),
                            new JProperty("Duration", dicItem.Value.Duration.TotalSeconds.ToString("0:0.00")),
                            new JProperty("Exception", dicItem.Value.Exception?.Message),
                            new JProperty("Data", new JObject(dicItem.Value.Data.Select(dicData =>
                                new JProperty(dicData.Key, dicData.Value))))
                        ))))
                ));
            return httpContext.Response.WriteAsync(json.ToString(Formatting.Indented));
        }
    }
}