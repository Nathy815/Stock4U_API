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
    public class GetEquityChartQueryHandler : IRequestHandler<GetEquityChartQuery, List<ListEquityChartsVM>>
    {
        private readonly SqlContext _context;
        private readonly IMediator _mediator;
        private readonly IMemoryCache _cache;

        public GetEquityChartQueryHandler(SqlContext context, IMediator mediator, IMemoryCache cache)
        {
            _context = context;
            _mediator = mediator;
            _cache = cache;
        }

        public async Task<List<ListEquityChartsVM>> Handle(GetEquityChartQuery request, CancellationToken cancellationToken)
        {
            var _userEquity = await _context.Set<UserEquity>()
                                            .Include(e => e.Equity)
                                            .Include(e => e.EquitiesToCompare)
                                                .ThenInclude(e => e.Equity)
                                            .Where(e => e.UserID == request.UserID &&
                                                        e.EquityID == request.EquityID)
                                            .FirstOrDefaultAsync();

            var _lista = new List<ListEquityChartsVM>();
            if (!_cache.TryGetValue(_userEquity.Equity.Id.ToString() + "_" + request.Filter, out ListEquityChartsVM chart))
            {
                var _chart = await _mediator.Send(new GenerateChartQuery(_userEquity.Equity.Ticker, request.Filter));
                var _obj = new ListEquityChartsVM(_userEquity.Equity.Ticker, _chart);
                _cache.Set(_userEquity.Equity.Id.ToString() + "_" + request.Filter, _obj, TimeSpan.FromMinutes(10));

                _lista.Add(_obj);
            }
            else
                _lista.Add(chart);

            foreach (var _equity in _userEquity.EquitiesToCompare)
            {
                if (!_cache.TryGetValue(_equity.Equity.Id.ToString() + "_" + request.Filter, out ListEquityChartsVM compare))
                {
                    var _chart = await _mediator.Send(new GenerateChartQuery(_equity.Equity.Ticker, request.Filter));
                    var _obj = new ListEquityChartsVM(_equity.Equity.Ticker, _chart);
                    _cache.Set(_userEquity.Equity.Id.ToString() + "_" + request.Filter, _obj, TimeSpan.FromMinutes(10));

                    _lista.Add(_obj);
                }
                else
                    _lista.Add(compare);
            }

            return _lista;
        }
    }
}