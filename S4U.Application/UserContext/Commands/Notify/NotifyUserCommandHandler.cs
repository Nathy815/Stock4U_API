using MediatR;
using S4U.Domain.Entities;
using S4U.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace S4U.Application.UserContext.Commands.Notify
{
    public class NotifyUserCommandHandler : IRequestHandler<NotifyUserCommand, bool>
    {
        private readonly SqlContext _context;

        public NotifyUserCommandHandler(SqlContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(NotifyUserCommand request, CancellationToken cancellationToken)
        {
            var _id = Guid.NewGuid();

            await _context.Notifications.AddAsync(new Notification
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Body = request.Body,
                RedirectID = request.RedirectID,
                UserID = request.UserID
            }, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}