using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.UserContext.Commands.Delete
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }
    }
}
