using ChallengeNubimetrics.Application.Auth;
using ChallengeNubimetrics.Domain.Entities;
using ChallengeNubimetrics.Infraestructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChallengeNubimetrics.Api.Registrations
{
    public static class IdentityRegistrationExtension
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddDefaultIdentity<User>()
                    .AddEntityFrameworkStores<DataContext>();

            var jwtSettings = new JwtSettings();
            _configuration.Bind(nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                    .AddJwtBearer(x =>
                    {
                        x.SaveToken = true;
                        x.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            RequireExpirationTime = false,
                            ValidateLifetime = true
                        };
                    });

            return services;
        }
    }
}
