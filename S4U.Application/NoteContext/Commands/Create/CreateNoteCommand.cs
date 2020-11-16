using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.NoteContext.Commands.Create
{
    public class CreateNoteCommand : IRequest<Guid>
    {
        public string Title { get; set; }
        public string Comments { get; set; }
        public IFormFile Attach { get; set; }
        public DateTime? Alert { get; set; }
        public Guid EquityID { get; set; }
        public Guid UserID { get; set; }
    }
}