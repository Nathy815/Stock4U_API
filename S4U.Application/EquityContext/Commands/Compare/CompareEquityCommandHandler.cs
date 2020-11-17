using MediatR;
using Microsoft.EntityFrameworkCore;
using S4U.Application.EquityContext.Commands.Create;
using S4U.Domain.Entities;
using S4U.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace S4U.Application.EquityContext.Commands.Compare
{
    public class CompareEquityCommandHandler : IRequestHandler<CompareEquityCommand, bool>
    {
        private readonly SqlContext _context;

        public CompareEquityCommandHandler(SqlContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(CompareEquityCommand request, CancellationToken cancellationToken)
        {
            var _equityID = Guid.NewGuid();
            var _equity = await _context.Set<Equity>()
                                        .Where(e => e.Ticker.Equals(request.Ticker))
                                        .FirstOrDefaultAsync();

            if (_equity == null)
            {
                await _context.Equities.AddAsync(new Equity
                {
                    Id = _equityID,
                    Ticker = request.Ticker,
                    Name = request.Name
                }, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);
            }
            else
                _equityID = _equity.Id;

            var _comparation = await _context.Set<CompareEquity>()
                                             .Where(e => e.CompareID == _equityID &&
                                                         e.EquityID == request.EquityID &&
                                                         e.UserID == request.UserID)
                                             .FirstOrDefaultAsync();

            if (_comparation == null)
            {
                await _context.CompareEquities.AddAsync(new CompareEquity
                {
                    CompareID = _equityID,
                    EquityID = request.EquityID,
                    UserID = request.UserID
                }, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);
            }

            return true;
        }
    }
}