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
    public class GetEquityChartQueryHandler : IRequestHandler<GetEquityChartQuery, List<ListEquityChartsVM>>
    {
        private readonly SqlContext _context;
        private readonly IMediator _mediator;

        public GetEquityChartQueryHandler(SqlContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<List<ListEquityChartsVM>> Handle(GetEquityChartQuery request, CancellationToken cancellationToken)
        {
            var _equities = await _context.Set<Equity>()
                                          .Where(e => request.Ids.Contains(e.Id))
                                          .ToListAsync();

            var _lista = new List<ListEquityChartsVM>();
            foreach (var _equity in _equities)
            {
                var _chart = await _mediator.Send(new GenerateChartQuery(_equity.Ticker, request.Filter));
                _lista.Add(new ListEquityChartsVM(_equity.Ticker, _chart));
            }

            return _lista;
        }
    }
}
