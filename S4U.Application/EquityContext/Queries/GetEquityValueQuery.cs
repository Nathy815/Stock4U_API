using MediatR;
using S4U.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.EquityContext.Queries
{
    public class GetEquityValueQuery : IRequest<List<GetEquityItemVM>>
    {
        public string Ticker { get; set; }

        public GetEquityValueQuery(string ticker)
        {
            Ticker = ticker;
        }
    }
}