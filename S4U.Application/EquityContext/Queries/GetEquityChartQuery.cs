using MediatR;
using S4U.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.EquityContext.Queries
{
    public class GetEquityChartQuery : IRequest<List<ListEquityChartsVM>>
    {
        public Guid EquityID { get; set; }
        public Guid UserID { get; set; }
        public string Filter { get; set; }
    }
}