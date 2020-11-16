using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.NoteContext.Commands.Delete
{
    public class DeleteNoteCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public DeleteNoteCommand(Guid id)
        {
            Id = id;
        }
    }
}