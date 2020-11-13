using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using S4U.Application.UserContext.Create;
using S4U.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S4U.API.Configurations
{
    public static class DependencyInjectionSetup
    {
        public static void RegisterHandlers(this IServiceCollection services)
        {
            services.AddScoped<SqlContext>();

            #region UserContext
            
            services.AddTransient<IRequestHandler<CreateUserCommand, Guid>, CreateUserCommandHandler>();

            services.AddTransient<IValidator<CreateUserCommand>, CreateUserCommandValidator>();
        
            #endregion
        }
    }
}
