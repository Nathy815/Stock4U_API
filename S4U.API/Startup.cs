using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using S4U.API.Configurations;
using MediatR;

namespace S4U.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    .AddFluentValidation(f => f.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddDatabaseSetup(Configuration);

            services.AddAuthenticationSetup();

            services.AddSwaggerSetup();

            services.AddSignalRSetup();

            services.RegisterHandlers();

            services.AddMediatR(typeof(Startup));

            services.AddHangfireSetup(Configuration);
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowAnyOrigin()
                       .AllowCredentials();
            });

            app.UseSwaggerDocs();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseSignalRSetup();
            app.UseHangfire(Configuration);
            app.UseMvc();
        }
    }
}