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

namespace S4U.Application.EquityContext.Commands.Remove
{
    public class RemoveCompareCommandHandler : IRequestHandler<RemoveCompareCommand, bool>
    {
        private readonly SqlContext _context;

        public RemoveCompareCommandHandler(SqlContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(RemoveCompareCommand request, CancellationToken cancellationToken)
        {
            var _compare = await _context.Set<CompareEquity>()
                                         .Where(e => e.UserID == request.UserID &&
                                                     e.EquityID == request.EquityID &&
                                                     e.CompareID == request.CompareID)
                                         .FirstOrDefaultAsync();

            _context.CompareEquities.Remove(_compare);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}