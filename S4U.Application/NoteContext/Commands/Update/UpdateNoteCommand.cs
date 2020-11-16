using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.NoteContext.Commands.Update
{
    public class UpdateNoteCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Comments { get; set; }
        public IFormFile Attach { get; set; }
        public DateTime? Alert { get; set; }
    }
}