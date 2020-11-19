using MediatR;
using Microsoft.EntityFrameworkCore;
using S4U.Application.UserContext.Commands.Notify;
using S4U.Application.Utils.Interfaces;
using S4U.Domain.Entities;
using S4U.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S4U.Application.Utils
{
    public class Hangfire : IHangfire
    {
        private readonly SqlContext _context;
        private readonly IMediator _mediator;

        public Hangfire(SqlContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task NotesAlerts()
        {
            var _notes = await _context.Set<Note>()
                                       .Include(n => n.UserEquity)
                                            .ThenInclude(ue => ue.User)
                                       .Include(n => n.UserEquity)
                                            .ThenInclude(ue => ue.Equity)
                                       .Where(n => !n.Deleted &&
                                                   n.Alert.HasValue &&
                                                   n.Alert.Value == DateTime.Now.Date)
                                       .ToListAsync();

            var _users = _notes.Select(n => n.UserEquity.User).Distinct().ToList();
            foreach (var _user in _users)
            {
                var _userNotes = _notes.Where(n => n.UserEquity.UserID == _user.Id).ToList();
                Guid? _id = null;
                if (_userNotes.Count == 1) _id = _userNotes.FirstOrDefault().Id;

                await _mediator.Send(new NotifyUserCommand
                {
                    Title = "Olá, " + _user.Name.Split(" ")[0],
                    Body = _userNotes.Count == 1 ?
                           "Vem dar uma espiada na nota que você criou para hoje! ;)" :
                           "Vem dar uma espiada nas " + _userNotes.Count.ToString() + " notas que você criou para hoje! ;)",
                    RedirectID = _id,
                    UserID = _user.Id
                });
            }
        }
    }
}