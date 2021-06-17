using ChallengeNubimetrics.Application.Auth;
using ChallengeNubimetrics.Application.Interfaces;
using ChallengeNubimetrics.Application.Profiles;
using ChallengeNubimetrics.Application.Services;
using ChallengeNubimetrics.Domain.Entities;
using ChallengeNubimetrics.Infraestructure.Interfaces;
using ChallengeNubimetrics.Infraestructure.Persistence;
using ChallengeNubimetrics.Infraestructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Formatting.Compact;
using System;
using System.Text;

namespace ChallengeNubimetrics.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options => options
                .UseSqlite(Configuration.GetConnectionString("LiteDb")));

            services.AddSingleton<Serilog.ILogger>(opt =>
            {
                return new LoggerConfiguration()
                    //.WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                    .WriteTo.File(
                        formatter: new CompactJsonFormatter(),
                        "logs.txt",
                        rollingInterval: RollingInterval.Day)
                    .WriteTo.Console()
                    .CreateLogger();
            });

            services.AddMediatR(AppDomain.CurrentDomain.Load("ChallengeNubimetrics.Application"));

            services.AddHttpClient("MELI_CountriesServiceUrl", c => c.BaseAddress = new System.Uri(Configuration.GetValue<string>("Integrations:MELI_CountriesServiceUrl")));
            services.AddHttpClient("MELI_SearchServiceUrl", c => c.BaseAddress = new System.Uri(Configuration.GetValue<string>("Integrations:MELI_SearchServiceUrl")));
            services.AddHttpClient("MELI_ConversionServiceUrl", c => c.BaseAddress = new System.Uri(Configuration.GetValue<string>("Integrations:MELI_ConversionServiceUrl")));
            services.AddHttpClient("MELI_CurrenciesServiceUrl", c => c.BaseAddress = new System.Uri(Configuration.GetValue<string>("Integrations:MELI_CurrenciesServiceUrl")));


            services.AddDefaultIdentity<User>()
                    .AddEntityFrameworkStores<DataContext>();

            var jwtSettings = new JwtSettings();
            Configuration.Bind(nameof(jwtSettings), jwtSettings);
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

            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IUserRepository, UserRepository>();

            services.AddAutoMapper(typeof(UserProfile));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ChallengeNubimetrics.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<User> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChallengeNubimetrics.Api v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            SeedDatabaseInitialData.SeedUsers(userManager);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
