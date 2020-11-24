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
    public class GetEquityChartQueryHandler : IRequestHandler<GetEquityChartQuery, List<List<GetEquityChartVM>>>
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

        public async Task<List<List<GetEquityChartVM>>> Handle(GetEquityChartQuery request, CancellationToken cancellationToken)
        {
            var _userEquity = await _context.Set<UserEquity>()
                                            .Include(e => e.Equity)
                                            .Include(e => e.EquitiesToCompare)
                                                .ThenInclude(e => e.Equity)
                                            .Where(e => e.UserID == request.UserID &&
                                                        e.EquityID == request.EquityID)
                                            .FirstOrDefaultAsync();

            var _lista = new List<List<GetEquityChartVM>>();
            if (!_cache.TryGetValue(_userEquity.Equity.Id.ToString() + "_" + request.Filter, out List<GetEquityChartVM> chart))
            {
                var _chart = await _mediator.Send(new GenerateChartQuery(_userEquity.Equity.Ticker, request.Filter));
                _cache.Set(_userEquity.Equity.Id.ToString() + "_" + request.Filter, _chart, TimeSpan.FromMinutes(10));

                _lista.Add(_chart);
            }
            else
                _lista.Add(chart);

            foreach (var _equity in _userEquity.EquitiesToCompare)
            {
                if (!_cache.TryGetValue(_equity.Equity.Id.ToString() + "_" + request.Filter, out List<GetEquityChartVM> compare))
                {
                    var _chart = await _mediator.Send(new GenerateChartQuery(_equity.Equity.Ticker, request.Filter));
                    _cache.Set(_userEquity.Equity.Id.ToString() + "_" + request.Filter, _chart, TimeSpan.FromMinutes(10));

                    _lista.Add(_chart);
                }
                else
                    _lista.Add(compare);
            }

            return _lista;
        }
    }
}