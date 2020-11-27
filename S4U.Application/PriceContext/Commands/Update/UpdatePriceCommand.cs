using MediatR;
using S4U.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.PriceContext.Commands.Update
{
    public class UpdatePriceCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public double Price { get; set; }
        public ePriceType Type { get; set; }
    }
}