using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using S4U.Application.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S4U.API.Configurations
{
    public static class SignalRSetup 
    {
        public static void AddSignalRSetup(this IServiceCollection services)
        {
            services.AddSignalR();
        }

        public static void UseSignalRSetup(this IApplicationBuilder app)
        {
            app.UseSignalR(endpoints =>
            {
                endpoints.MapHub<EquityHub>("/equityHub");
            });
        }
    }
}
