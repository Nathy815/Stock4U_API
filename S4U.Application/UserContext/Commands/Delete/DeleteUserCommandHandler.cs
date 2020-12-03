using MediatR;
using Microsoft.EntityFrameworkCore;
using S4U.Domain.Entities;
using S4U.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace S4U.Application.UserContext.Commands.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly SqlContext _context;

        public DeleteUserCommandHandler(SqlContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var _user = await _context.Set<User>()
                                      .Where(u => u.Id == request.Id)
                                      .FirstOrDefaultAsync();

            _user.Deleted = true;

            _context.Users.Update(_user);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}