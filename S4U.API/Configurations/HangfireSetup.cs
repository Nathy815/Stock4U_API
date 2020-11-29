using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using S4U.Application.Services.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace S4U.API.Configurations
{
    public static class HangfireSetup
    {
        public static void AddHangfireSetup(this IServiceCollection services, IConfiguration _configuration)
        {
            var _options = new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                UsePageLocksOnDequeue = true,
                DisableGlobalLocks = true
            };

            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(_configuration.GetConnectionString("SqlConnection"), _options));

            services.AddHangfireServer();

            #region ScheduleJobs

            var provider = services.BuildServiceProvider();
            var hangfireJobs = provider.GetService<IHangfire>();

            JobStorage.Current = new SqlServerStorage(_configuration.GetConnectionString("SqlConnection"), _options);

            RecurringJob.AddOrUpdate(() => hangfireJobs.GetRealTimeData(), Cron.Minutely);
            RecurringJob.AddOrUpdate(() => hangfireJobs.SendPushNotes(), Cron.Minutely);

            #endregion
        }

        public static void UseHangfire(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseHangfireServer();
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                DashboardTitle = "Stock4U - Hangfire",
                Authorization = new[]
                {
                    new HangfireAuthorizationFilter(configuration)
                }
            });
        }
    }

    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private readonly IConfiguration _configuration;

        public HangfireAuthorizationFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool Authorize(DashboardContext context)
        {
            var httpContext = ((AspNetCoreDashboardContext)context).HttpContext;

            try
            {
                var _token = httpContext.Request.Headers["Authorization"].FirstOrDefault();

                var options = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(_configuration["Authentication:SecurityKey"]))
                };

                var validator = new JwtSecurityTokenHandler();
                validator.ValidateToken(_token.Replace("Bearer ", ""), options, out var validatedToken);
                JwtSecurityToken securityToken = validatedToken as JwtSecurityToken;

                if (securityToken == null)
                    throw new Exception("Invalid JWT token.");
                else
                    validator.ReadJwtToken(_token);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
