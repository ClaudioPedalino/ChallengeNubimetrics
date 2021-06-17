using ChallengeNubimetrics.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChallengeNubimetrics.Api.Registrations
{
    public static class DatabaseRegistrationExtension
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddDbContext<DataContext>(options => options
                    .UseSqlite(_configuration.GetConnectionString("LiteDb")));

            return services;
        }
    }
}
