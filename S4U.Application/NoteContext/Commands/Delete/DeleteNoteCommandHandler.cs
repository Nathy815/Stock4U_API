using MediatR;
using Microsoft.EntityFrameworkCore;
using S4U.Domain.Entities;
using S4U.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace S4U.Application.NoteContext.Commands.Delete
{
    public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, bool>
    {
        private readonly SqlContext _context;

        public DeleteNoteCommandHandler(SqlContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            var _note = await _context.Set<Note>()
                                      .Where(e => e.Id == request.Id)
                                      .FirstOrDefaultAsync();

            _note.Deleted = true;

            _context.Notes.Update(_note);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}