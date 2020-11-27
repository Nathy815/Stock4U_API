using MediatR;
using S4U.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.PriceContext.Commands.Create
{
    public class CreatePriceCommand : IRequest<bool>
    {
        public Guid UserID { get; set; }
        public Guid EquityID { get; set; }
        public double Price { get; set; }
        public ePriceType Type { get; set; }
    }
}