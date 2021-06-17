using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Formatting.Compact;

namespace ChallengeNubimetrics.Api.Registrations
{
    public static class LoggingRegistrationExtension
    {
        public static IServiceCollection AddLogger(this IServiceCollection services)
        {
            services.AddSingleton<Serilog.ILogger>(opt =>
            {
                return new LoggerConfiguration()
                    .WriteTo.File(formatter: new CompactJsonFormatter(),
                                  path: "logs.txt",
                                  rollingInterval: RollingInterval.Day)
                    .WriteTo.Console()
                    .CreateLogger();
            });

            return services;
        }
    }
}
