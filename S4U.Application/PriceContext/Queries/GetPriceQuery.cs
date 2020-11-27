using MediatR;
using S4U.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.PriceContext.Queries
{
    public class GetPriceQuery : IRequest<GetPriceVM>
    {
        public Guid Id { get; set; }

        public GetPriceQuery(Guid id)
        {
            Id = id;
        }
    }
}