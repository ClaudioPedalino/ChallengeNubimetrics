using ChallengeNubimetrics.Application.Interfaces;
using ChallengeNubimetrics.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChallengeNubimetrics.Api.Registrations
{
    public static class ServicesRegistrationExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IProducerService, ProducerService>();

            return services;
        }
    }
}
