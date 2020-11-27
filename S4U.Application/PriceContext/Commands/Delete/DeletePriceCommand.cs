using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.PriceContext.Commands.Delete
{
    public class DeletePriceCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public DeletePriceCommand(Guid id)
        {
            Id = id;
        }
    }
}