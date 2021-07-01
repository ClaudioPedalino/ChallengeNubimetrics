using ChallengeNubimetrics.Infraestructure.Interfaces;
using ChallengeNubimetrics.Infraestructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChallengeNubimetrics.Api.Registrations
{
    public static class RepositoriesRegistrationExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}