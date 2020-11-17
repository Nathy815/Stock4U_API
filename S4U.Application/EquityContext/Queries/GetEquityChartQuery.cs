using MediatR;
using S4U.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.EquityContext.Queries
{
    public class GetEquityChartQuery : IRequest<GetEquityChartVM>
    {
        public Guid Id { get; set; }

        public GetEquityChartQuery(Guid id)
        {
            Id = id;
        }
    }
}