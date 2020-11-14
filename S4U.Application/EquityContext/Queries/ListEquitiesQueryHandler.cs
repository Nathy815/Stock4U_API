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

namespace S4U.Application.EquityContext.Queries
{
    public class ListEquitiesQueryHandler : IRequestHandler<ListEquitiesQuery, List<GetEquityVM>>
    {
        private readonly SqlContext _context;
        private readonly IMediator _mediator;

        public ListEquitiesQueryHandler(SqlContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<List<GetEquityVM>> Handle(ListEquitiesQuery request, CancellationToken cancellationToken)
        {
            var _equities = await _context.Set<User>()
                                          .Include(u => u.UsersEquities)
                                            .ThenInclude(u => u.Equity)
                                          .Where(u => u.Id == request.UserID)
                                          .Select(u => u.UsersEquities)
                                          .FirstOrDefaultAsync();

            if (_equities == null || _equities.Count == 0) return null;

            var _list = new List<GetEquityVM>();
            foreach (var _equity in _equities)
            {
                var _yahoo = await _mediator.Send(new GetEquityValueQuery(_equity.Equity.Ticker));
                _list.Add(new GetEquityVM(_equity.Equity, _yahoo));
            }

            return _list;
        }
    }
}