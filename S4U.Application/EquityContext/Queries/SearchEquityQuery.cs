using MediatR;
using S4U.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.EquityContext.Queries
{
    public class SearchEquityQuery : IRequest<List<SearchEquityVM>>
    {
        public string Term { get; set; }

        public SearchEquityQuery(string term)
        {
            Term = term;
        }
    }
}