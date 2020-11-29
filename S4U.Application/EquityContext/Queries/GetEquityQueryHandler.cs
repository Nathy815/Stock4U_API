using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using S4U.Domain.Entities;
using S4U.Domain.ViewModels;
using S4U.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace S4U.Application.EquityContext.Queries
{
    public class GetEquityQueryHandler : IRequestHandler<GetEquityQuery, GetEquityVM>
    {
        private readonly SqlContext _context;
        private readonly IMediator _mediator;
        private readonly IMemoryCache _cache;

        public GetEquityQueryHandler(SqlContext context, IMediator mediator, IMemoryCache cache)
        {
            _context = context;
            _mediator = mediator;
            _cache = cache;
        }

        public async Task<GetEquityVM> Handle(GetEquityQuery request, CancellationToken cancellationToken)
        {
            var _userEquity = await _context.Set<UserEquity>()
                                            .Include(e => e.Equity)
                                            .Include(e => e.Notes)
                                            .Include(e => e.EquitiesToCompare)
                                                .ThenInclude(e => e.Equity)
                                            .Where(e => e.EquityID == request.EquityID &&
                                                        e.UserID == request.UserID)
                                            .FirstOrDefaultAsync();

            var _yahoo = await _mediator.Send(new GetEquityValueQuery(_userEquity.Equity.Ticker));
            var _result = new GetEquityVM(_userEquity.Equity, _yahoo);

            foreach (var _equity in _userEquity.EquitiesToCompare)
            {
                _yahoo = await _mediator.Send(new GetEquityValueQuery(_equity.Equity.Ticker));
                _result.Compare.Add(new GetEquityCompareVM(_equity.Equity, _yahoo));
            }

            return _result;
        }
    }
}
