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

namespace S4U.Application.EquityContext.Commands.Create
{
    public class CreateEquityCommandHandler : IRequestHandler<CreateEquityCommand, Guid>
    {
        private readonly SqlContext _context;

        public CreateEquityCommandHandler(SqlContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateEquityCommand request, CancellationToken cancellationToken)
        {
            var _equity = await _context.Set<Equity>()
                                        .Include(e => e.UsersEquities)
                                        .Where(e => e.Ticker.Equals(request.Ticker))
                                        .FirstOrDefaultAsync();

            if (_equity == null)
            {
                var _id = Guid.NewGuid();

                await _context.Equities.AddAsync(new Equity
                {
                    Id = _id,
                    Ticker = request.Ticker,
                    Name = request.Name,
                    UsersEquities = new List<UserEquity>()
                    {
                        new UserEquity
                        {
                            EquityID = _id,
                            UserID = request.UserID
                        }
                    }
                }, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                return _id;
            }
            else
            {
                var _user = _equity.UsersEquities.Where(u => u.UserID == request.UserID).FirstOrDefault();

                if (_user == null)
                {
                    await _context.UserEquities.AddAsync(new UserEquity
                    {
                        EquityID = _equity.Id,
                        UserID = request.UserID
                    }, cancellationToken);

                    await _context.SaveChangesAsync(cancellationToken);
                }
            }

            return _equity.Id;
        }
    }
}