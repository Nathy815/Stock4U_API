using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using S4U.Application.EquityContext.Queries;
using S4U.Application.Hubs;
using S4U.Application.Services.Interface;
using S4U.Domain.Entities;
using S4U.Domain.ViewModels;
using S4U.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S4U.Application.Services
{
    public class Hangfire : IHangfire
    {
        private readonly SqlContext _context;
        private readonly IMediator _mediator;
        private readonly IMemoryCache _cache;
        private readonly EquityHub _hub;

        public Hangfire(SqlContext context, IMediator mediator, IMemoryCache cache, EquityHub hub)
        {
            _context = context;
            _mediator = mediator;
            _cache = cache;
            _hub = hub;
        }

        public async Task GetRealTimeData()
        {
            var _equities = await _context.Set<Equity>()
                                          .Where(e => !e.Deleted)
                                          .ToListAsync();

            foreach (var _equity in _equities)
            {
                var _yahoo = await _mediator.Send(new GetEquityValueQuery(_equity.Ticker));
                var _equityModel = new GetEquityVM(_equity, _yahoo);
                _cache.Set<GetEquityVM>(_equity.Id.ToString(), _equityModel);
            }
            
            await SendRealTime();
        }

        public async Task SendRealTime()
        {
            var _users = await _context.Set<User>()
                                       .Include(u => u.UsersEquities)
                                            .ThenInclude(ue => ue.Equity)
                                       .Where(u => !u.Deleted)
                                       .ToListAsync();

            foreach (var _user in _users)
                await _hub.SendMessage(await _mediator.Send(new ListEquitiesQuery(_user.Id)));
        }
    }
}
