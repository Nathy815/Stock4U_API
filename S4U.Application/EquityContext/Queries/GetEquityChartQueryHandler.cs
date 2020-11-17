using MediatR;
using S4U.Domain.ViewModels;
using S4U.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace S4U.Application.EquityContext.Queries
{
    public class GetEquityChartQueryHandler : IRequestHandler<GetEquityChartQuery, GetEquityChartVM>
    {
        private readonly SqlContext _context;

        public GetEquityChartQueryHandler(SqlContext context)
        {
            _context = context;
        }

        public async Task<GetEquityChartVM> Handle(GetEquityChartQuery request, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
