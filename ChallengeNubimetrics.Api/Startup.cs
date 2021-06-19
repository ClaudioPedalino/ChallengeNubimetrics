using ChallengeNubimetrics.Api.Middelwares;
using ChallengeNubimetrics.Api.Registrations;
using ChallengeNubimetrics.Application.PipelineBehaviors;
using ChallengeNubimetrics.Application.Profiles;
using ChallengeNubimetrics.Application.Queries.Search;
using ChallengeNubimetrics.Application.Services;
using ChallengeNubimetrics.Domain.Entities;
using ChallengeNubimetrics.Infraestructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

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
            services.AddIdentity(Configuration);

            services.AddDatabase(Configuration);
            services.AddLogger();

            services.AddExternalServices(Configuration);
            services.AddRepositories(Configuration);
            services.AddServices(Configuration);
            services.AddHostedService<PrinterHostedService>();

            services.AddMediatR(AppDomain.CurrentDomain.Load("ChallengeNubimetrics.Application"));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehaviour<,>));

            services.AddAutoMapper(typeof(UserProfile));

            services.AddControllers();
            services.AddSwagger();

            //services.AddHealthCheck(Configuration);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<User> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseRouting();

            app.UseAuthorization();

            SeedDatabaseInitialData.SeedUsers(userManager);

            //app.UseMiddleware<RequestResponseLoggingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseHealhtChecks();
        }


    }
}
