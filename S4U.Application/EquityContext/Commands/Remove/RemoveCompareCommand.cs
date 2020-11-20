using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.EquityContext.Commands.Remove
{
    public class RemoveCompareCommand : IRequest<bool>
    {
        public Guid UserID { get; set; }
        public Guid EquityID { get; set; }
        public Guid CompareID { get; set; }
    }
}