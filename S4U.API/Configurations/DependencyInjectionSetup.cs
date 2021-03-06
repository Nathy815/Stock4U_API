﻿using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using S4U.Application.EquityContext.Commands.Compare;
using S4U.Application.EquityContext.Commands.Create;
using S4U.Application.EquityContext.Commands.Delete;
using S4U.Application.EquityContext.Commands.Remove;
using S4U.Application.EquityContext.Queries;
using S4U.Application.Hubs;
using S4U.Application.NoteContext.Commands.Create;
using S4U.Application.NoteContext.Commands.Delete;
using S4U.Application.NoteContext.Commands.Update;
using S4U.Application.NoteContext.Queries;
using S4U.Application.PriceContext.Commands.Create;
using S4U.Application.PriceContext.Commands.Delete;
using S4U.Application.PriceContext.Commands.Update;
using S4U.Application.PriceContext.Queries;
using S4U.Application.Services.Interface;
using S4U.Application.UserContext.Commands.Create;
using S4U.Application.UserContext.Commands.Delete;
using S4U.Application.UserContext.Commands.Notify;
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

            services.AddTransient<IHangfire, Application.Services.Hangfire>();

            #region EquityContext

            services.AddTransient<IRequestHandler<CompareEquityCommand, bool>, CompareEquityCommandHandler>()
                    .AddTransient<IRequestHandler<CreateEquityCommand, Guid>, CreateEquityCommandHandler>()
                    .AddTransient<IRequestHandler<DeleteUserEquityCommand, bool>, DeleteUserEquityCommandHandler>()
                    .AddTransient<IRequestHandler<GenerateChartQuery, List<GetEquityChartVM>>, GenerateChartQueryHandler>()
                    .AddTransient<IRequestHandler<GetEquityChartQuery, List<List<GetEquityChartVM>>>, GetEquityChartQueryHandler>()
                    .AddTransient<IRequestHandler<GetEquityQuery, GetEquityVM>, GetEquityQueryHandler>()
                    .AddTransient<IRequestHandler<GetEquityValueQuery, List<GetEquityItemVM>>, GetEquityValueQueryHandler>()
                    .AddTransient<IRequestHandler<ListEquitiesQuery, List<GetEquityCompareVM>>, ListEquitiesQueryHandler>()
                    .AddTransient<IRequestHandler<RemoveCompareCommand, bool>, RemoveCompareCommandHandler>()
                    .AddTransient<IRequestHandler<SearchEquityQuery, List<SearchEquityVM>>, SearchEquityQueryHandler>();

            services.AddTransient<IValidator<CompareEquityCommand>, CompareEquityCommandValidator>()
                    .AddTransient<IValidator<CreateEquityCommand>, CreateEquityCommandValidator>();

            #endregion

            #region NoteContext

            services.AddTransient<IRequestHandler<CreateNoteCommand, Guid>, CreateNoteCommandHandler>()
                    .AddTransient<IRequestHandler<DeleteNoteCommand, bool>, DeleteNoteCommandHandler>()
                    .AddTransient<IRequestHandler<GetNoteQuery, GetNoteVM>, GetNoteQueryHandler>()
                    .AddTransient<IRequestHandler<ListNotesQuery, List<GetNoteVM>>, ListNotesQueryHandler>()
                    .AddTransient<IRequestHandler<UpdateNoteCommand, bool>, UpdateNoteCommandHandler>();

            services.AddTransient<IValidator<CreateNoteCommand>, CreateNoteCommandValidator>()
                    .AddTransient<IValidator<UpdateNoteCommand>, UpdateNoteCommandValidator>();

            #endregion

            #region PriceContext

            services.AddTransient<IRequestHandler<CreatePriceCommand, bool>, CreatePriceCommandHandler>()
                    .AddTransient<IRequestHandler<DeletePriceCommand, bool>, DeletePriceCommandHandler>()
                    .AddTransient<IRequestHandler<GetPriceQuery, GetPriceVM>, GetPriceQueryHandler>()
                    .AddTransient<IRequestHandler<ListPricesQuery, List<GetPriceVM>>, ListPricesQueryHandler>()
                    .AddTransient<IRequestHandler<UpdatePriceCommand, bool>, UpdatePriceCommandHandler>();

            services.AddTransient<IValidator<CreatePriceCommand>, CreatePriceCommandValidator>()
                    .AddTransient<IValidator<UpdatePriceCommand>, UpdatePriceCommandValidator>();

            #endregion

            #region UserContext

            services.AddTransient<IRequestHandler<CreateUserCommand, Guid>, CreateUserCommandHandler>()
                    .AddTransient<IRequestHandler<DeleteUserCommand, bool>, DeleteUserCommandHandler>()
                    .AddTransient<IRequestHandler<FindByEmailQuery, Guid>, FindByEmailQueryHandler>()
                    .AddTransient<IRequestHandler<GetAddressQuery, GetAddressVM>, GetAddressQueryHandler>()
                    .AddTransient<IRequestHandler<GetUserQuery, GetUserVM>, GetUserQueryHandler>()
                    .AddTransient<IRequestHandler<NotifyUserCommand, bool>, NotifyUserCommandHandler>()
                    .AddTransient<IRequestHandler<UpdateUserCommand, bool>, UpdateUserCommandHandler>();

            services.AddTransient<IValidator<CreateUserCommand>, CreateUserCommandValidator>()
                    .AddTransient<IValidator<UpdateUserCommand>, UpdateUserCommandValidator>();

            #endregion
        }
    }
}