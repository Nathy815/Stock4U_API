using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.EquityContext.Commands.Delete
{
    public class DeleteUserEquityCommand : IRequest<bool>
    {
        public Guid UserID { get; set; }
        public Guid EquityID { get; set; }
    }
}