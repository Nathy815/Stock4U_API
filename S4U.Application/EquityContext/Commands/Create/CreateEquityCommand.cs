using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.EquityContext.Commands.Create
{
    public class CreateEquityCommand : IRequest<Guid>
    {
        public string Ticker { get; set; }
        public string Name { get; set; }
        public Guid UserID { get; set; }
    }
}