using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.EquityContext.Commands.Compare
{
    public class CompareEquityCommand : IRequest<bool>
    {
        public string Ticker { get; set; }
        public string Name { get; set; }
        public Guid EquityID { get; set; }
        public Guid UserID { get; set; }
    }
}