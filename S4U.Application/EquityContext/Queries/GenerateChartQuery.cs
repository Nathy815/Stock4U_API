using MediatR;
using S4U.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.EquityContext.Queries
{
    public class GenerateChartQuery : IRequest<List<GetEquityChartVM>>
    {
        public string Ticker { get; set; }
        public string Filter { get; set; }

        public GenerateChartQuery(string ticker, string filter)
        {
            Ticker = ticker;
            Filter = filter;
        }
    }
}