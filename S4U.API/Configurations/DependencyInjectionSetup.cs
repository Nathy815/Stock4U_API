using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using S4U.Application.EquityContext.Commands.Create;
using S4U.Application.EquityContext.Queries;
using S4U.Application.UserContext.Commands.Create;
using S4U.Application.UserContext.Commands.Update;
using S4U.Application.UserContext.Queries;
using S4U.Domain.ViewModels;
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

            #region EquityContext

            services.AddTransient<IRequestHandler<CreateEquityCommand, Guid>, CreateEquityCommandHandler>()
                    .AddTransient<IRequestHandler<GetEquityValueQuery, Tuple<double, bool?>>, GetEquityValueQueryHandler>()
                    .AddTransient<IRequestHandler<ListEquitiesQuery, List<GetEquityVM>>, ListEquitiesQueryHandler>()
                    .AddTransient<IRequestHandler<SearchEquityQuery, List<SearchEquityVM>>, SearchEquityQueryHandler>();

            services.AddTransient<IValidator<CreateEquityCommand>, CreateEquityCommandValidator>();

            #endregion

            #region UserContext

            services.AddTransient<IRequestHandler<CreateUserCommand, Guid>, CreateUserCommandHandler>()
                    .AddTransient<IRequestHandler<FindByEmailQuery, Guid>, FindByEmailQueryHandler>()
                    .AddTransient<IRequestHandler<GetAddressQuery, GetAddressVM>, GetAddressQueryHandler>()
                    .AddTransient<IRequestHandler<GetUserQuery, GetUserVM>, GetUserQueryHandler>()
                    .AddTransient<IRequestHandler<UpdateUserCommand, bool>, UpdateUserCommandHandler>();

            services.AddTransient<IValidator<CreateUserCommand>, CreateUserCommandValidator>()
                    .AddTransient<IValidator<UpdateUserCommand>, UpdateUserCommandValidator>();

            #endregion
        }
    }
}