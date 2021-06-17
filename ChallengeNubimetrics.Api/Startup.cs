using ChallengeNubimetrics.Api.Registrations;
using ChallengeNubimetrics.Application.Profiles;
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
            services.AddDatabase(Configuration);

            services.AddMediatR(AppDomain.CurrentDomain.Load("ChallengeNubimetrics.Application"));

            services.AddLogger();

            services.AddExternalServices(Configuration);

            services.AddRepositories(Configuration);

            services.AddServices(Configuration);

            services.AddIdentity(Configuration);

            services.AddAutoMapper(typeof(UserProfile));

            services.AddControllers();

            services.AddSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<User> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerDocumentation();

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
