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

namespace S4U.Application.PriceContext.Commands.Update
{
    public class UpdatePriceCommandHandler : IRequestHandler<UpdatePriceCommand, bool>
    {
        private readonly SqlContext _context;

        public UpdatePriceCommandHandler(SqlContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(UpdatePriceCommand request, CancellationToken cancellationToken)
        {
            var _price = await _context.Set<UserEquityPrice>()
                                       .Where(e => e.Id == request.Id)
                                       .FirstOrDefaultAsync();

            _price.Price = request.Price;
            _price.Type = request.Type;
            _price.Sent = false;

            _context.UserEquityPrices.Update(_price);

            await _context.SaveChangesAsync(cancellationToken);
            
            return true;
        }
    }
}
