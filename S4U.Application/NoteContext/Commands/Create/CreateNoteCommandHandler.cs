using MediatR;
using S4U.Domain.Entities;
using S4U.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace S4U.Application.NoteContext.Commands.Create
{
    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Guid>
    {
        private readonly SqlContext _context;

        public CreateNoteCommandHandler(SqlContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var _id = Guid.NewGuid();

            await _context.Notes.AddAsync(new Note
            {
                Id = _id,
                Title = request.Title,
                Comments = request.Comments,
                Attach = null,
                Alert = request.Alert,
                UserID = request.UserID,
                EquityID = request.EquityID
            }, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return _id;
        }
    }
}
