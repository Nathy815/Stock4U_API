using MediatR;
using Microsoft.EntityFrameworkCore;
using S4U.Domain.Entities;
using S4U.Domain.ViewModels;
using S4U.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace S4U.Application.PriceContext.Queries
{
    public class GetPriceQueryHandler : IRequestHandler<GetPriceQuery, GetPriceVM>
    {
        private readonly SqlContext _context;

        public GetPriceQueryHandler(SqlContext context)
        {
            _context = context;
        }

        public async Task<GetPriceVM> Handle(GetPriceQuery request, CancellationToken cancellationToken)
        {
            return await _context.Set<UserEquityPrice>()
                                 .Where(e => e.Id == request.Id)
                                 .Select(e => new GetPriceVM
                                 {
                                     Id = e.Id,
                                     Price = e.Price,
                                     Type = e.Type
                                 })
                                 .FirstOrDefaultAsync();
        }
    }
}