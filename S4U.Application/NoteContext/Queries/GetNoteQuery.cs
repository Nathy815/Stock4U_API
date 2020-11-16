using MediatR;
using S4U.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.NoteContext.Queries
{
    public class GetNoteQuery : IRequest<GetNoteVM>
    {
        public Guid Id { get; set; }

        public GetNoteQuery(Guid id)
        {
            Id = id;
        }
    }
}