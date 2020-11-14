using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.EquityContext.Queries
{
    public class GetEquityValueQuery : IRequest<Tuple<double, bool?>>
    {
        public string Ticker { get; set; }

        public GetEquityValueQuery(string ticker)
        {
            Ticker = ticker;
        }
    }
}