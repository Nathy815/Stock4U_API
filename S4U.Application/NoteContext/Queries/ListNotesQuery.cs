using MediatR;
using S4U.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.NoteContext.Queries
{
    public class ListNotesQuery : IRequest<List<GetNoteVM>>
    {
        public Guid UserID { get; set; }
        public Guid EquityID { get; set; }
    }
}