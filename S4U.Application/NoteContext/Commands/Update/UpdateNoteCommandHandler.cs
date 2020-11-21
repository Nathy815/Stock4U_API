using Firebase.Storage;
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

namespace S4U.Application.NoteContext.Commands.Update
{
    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, bool>
    {
        private readonly SqlContext _context;

        public UpdateNoteCommandHandler(SqlContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            var _note = await _context.Set<Note>()
                                      .Where(e => e.Id == request.Id)
                                      .FirstOrDefaultAsync();

            _note.Title = request.Title;
            _note.Comments = request.Comments;
            _note.Attach = request.Attach == null ? null :
                           await new FirebaseStorage("stock4u-f97f2.appspot.com").Child("notes").Child(_note.Id.ToString()).PutAsync(request.Attach.OpenReadStream());
            _note.Alert = request.Alert;

            _context.Notes.Update(_note);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
