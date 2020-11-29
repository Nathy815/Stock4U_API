using Hangfire;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using S4U.Application.EquityContext.Queries;
using S4U.Application.Hubs;
using S4U.Application.Services.Interface;
using S4U.Application.UserContext.Commands.Notify;
using S4U.Domain.Entities;
using S4U.Domain.Enums;
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
        private readonly IHubContext<EquityHub> _hub;

        public Hangfire(SqlContext context, IMediator mediator, IMemoryCache cache, IHubContext<EquityHub> hub)
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
                BackgroundJob.Enqueue(() => SendPushAlerts(_equity.Id, _equity.Ticker, _yahoo.ElementAt(3).value));
                var _equityModel = new GetEquityVM(_equity, _yahoo);
                _cache.Set<GetEquityVM>(_equity.Id.ToString(), _equityModel);
            }
            
            await SendRealTime();
        }

        public async Task SendPushNotes()
        {
            var _users = await _context.Set<User>()
                                       .Where(u => !u.Deleted && 
                                                   !string.IsNullOrEmpty(u.PushToken))
                                       .ToListAsync();

            foreach (var _user in _users)
            {
                var _now = DateTime.Now;
                var _initial = new DateTime(_now.Year, _now.Month, _now.Day, _now.Hour, _now.Minute, 0);
                var _final = new DateTime(_now.Year, _now.Month, _now.Day, _now.Hour, _now.Minute, 59);

                var _notes = await _context.Set<Note>()
                                           .Include(n => n.UserEquity)
                                                .ThenInclude(n => n.Equity)
                                           .Where(n => !n.Deleted &&
                                                       !n.Sent &&
                                                       n.UserID == _user.Id &&
                                                       n.Alert.HasValue &&
                                                       n.Alert.Value >= _initial &&
                                                       n.Alert.Value <= _final)
                                           .ToListAsync();

                if (_notes != null && _notes.Count > 0)
                {
                    await _mediator.Send(new NotifyUserCommand()
                    {
                        Title = _notes.Count == 1 ? "Alerta de Nota" : "Alertas de Notas",
                        Body = _notes.Count == 1 ? 
                               string.Format("Clique para verificar a nota '{0}' da ação {1}", _notes[0].Title, _notes[0].UserEquity.Equity.Ticker) :
                               string.Format("Clique para visualizar {0} notas da ação {1}", _notes.Count, _notes[0].UserEquity.Equity.Ticker),
                        RedirectID = _notes.Count == 1 ? _notes[0].Id : _notes[0].UserEquity.EquityID,
                        RedirectType = _notes.Count == 1 ? eRedirectType.Note : eRedirectType.ListNotes,
                        UserID = _user.Id
                    });

                    foreach (var _note in _notes)
                        _note.Sent = true;

                    _context.Notes.UpdateRange(_notes);

                    await _context.SaveChangesAsync();
                }
            }
        }

        private async Task SendPushAlerts(Guid equityID, string name, double value)
        {
            var _pushes = await _context.Set<UserEquityPrice>()
                                        .Include(e => e.UserEquity)
                                            .ThenInclude(e => e.User)
                                        .Where(e => !e.Deleted &&
                                                    e.EquityID == equityID &&
                                                    !e.Sent &&
                                                    (e.Type == ePriceType.MaiorOuIgual && value >= e.Price) ||
                                                    (e.Type == ePriceType.MenorOuIgual && value <= e.Price))
                                        .ToListAsync();

            foreach (var _push in _pushes)
            {
                if (!string.IsNullOrEmpty(_push.UserEquity.User.PushToken))
                {
                    await _mediator.Send(new NotifyUserCommand()
                    {
                        Title = "Alerta de Preço",
                        Body = string.Format("A ação {0} atingiu o valor {1} a {2}", name, _push.Type == ePriceType.MaiorOuIgual ? "maior ou igual" : "menor ou igual", value.ToString()),
                        UserID = _push.UserID,
                        RedirectID = equityID,
                        RedirectType = eRedirectType.Equity
                    });

                    _push.Sent = true;

                    _context.UserEquityPrices.Update(_push);
                }
            }

            await _context.SaveChangesAsync();
        }

        private async Task SendRealTime()
        {
            var _users = await _context.Set<User>()
                                       .Include(u => u.UsersEquities)
                                            .ThenInclude(ue => ue.Equity)
                                       .Where(u => !u.Deleted)
                                       .ToListAsync();

            foreach (var _user in _users)
                await _hub.Clients.Client(_user.Id.ToString()).SendAsync("ListEquities",
                    await _mediator.Send(new ListEquitiesQuery(_user.Id)));
        }
    }
}
