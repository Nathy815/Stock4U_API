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

namespace S4U.Application.EquityContext.Commands.Delete
{
    public class DeleteUserEquityCommandHandler : IRequestHandler<DeleteUserEquityCommand, bool>
    {
        private readonly SqlContext _context;

        public DeleteUserEquityCommandHandler(SqlContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteUserEquityCommand request, CancellationToken cancellationToken)
        {
            var _userEquity = await _context.Set<UserEquity>()
                                            .Where(e => e.EquityID == request.EquityID &&
                                                        e.UserID == request.UserID)
                                            .FirstOrDefaultAsync();

            _context.UserEquities.Remove(_userEquity);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
