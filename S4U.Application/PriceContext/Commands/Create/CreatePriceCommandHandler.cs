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

namespace S4U.Application.PriceContext.Commands.Create
{
    public class CreatePriceCommandHandler : IRequestHandler<CreatePriceCommand, bool>
    {
        private readonly SqlContext _context;

        public CreatePriceCommandHandler(SqlContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(CreatePriceCommand request, CancellationToken cancellationToken)
        {
            var _userEquity = await _context.Set<UserEquity>()
                                            .Include(e => e.Prices)
                                            .Where(e => e.UserID == request.UserID &&
                                                        e.EquityID == request.EquityID)
                                            .FirstOrDefaultAsync();

            var _parameter = _userEquity.Prices.Where(e => e.Price == request.Price && e.Type == request.Type).FirstOrDefault();
            if (_parameter == null)
            {
                await _context.UserEquityPrices.AddAsync(new UserEquityPrice
                {
                    Id = Guid.NewGuid(),
                    UserID = request.UserID,
                    EquityID = request.EquityID,
                    Price = request.Price,
                    Type = request.Type
                }, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);
            }

            return true;
        }
    }
}
