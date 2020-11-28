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
    public class ListEquitiesQueryHandler : IRequestHandler<ListEquitiesQuery, List<GetEquityCompareVM>>
    {
        private readonly SqlContext _context;
        private readonly IMediator _mediator;
        private readonly IMemoryCache _cache;

        public ListEquitiesQueryHandler(SqlContext context, IMediator mediator, IMemoryCache cache)
        {
            _context = context;
            _mediator = mediator;
            _cache = cache;
        }

        public async Task<List<GetEquityCompareVM>> Handle(ListEquitiesQuery request, CancellationToken cancellationToken)
        {
            var _user = await _context.Set<User>()
                                      .Include(u => u.UsersEquities)
                                        .ThenInclude(u => u.Equity)
                                      .Where(u => u.Id == request.UserID)
                                      .FirstOrDefaultAsync();

            if (_user.UsersEquities == null || _user.UsersEquities.Count == 0) return null;

            var _list = new List<GetEquityCompareVM>();
            foreach (var _equity in _user.UsersEquities)
            {
                var _yahoo = await _mediator.Send(new GetEquityValueQuery(_equity.Equity.Ticker));
                _list.Add(new GetEquityCompareVM(_equity.Equity, _yahoo));
            }

            return _list;
        }
    }
}