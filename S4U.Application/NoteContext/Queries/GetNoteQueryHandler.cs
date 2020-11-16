using MediatR;
using Microsoft.EntityFrameworkCore;
using S4U.Domain.Entities;
using S4U.Domain.ViewModels;
using S4U.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace S4U.Application.NoteContext.Queries
{
    public class GetNoteQueryHandler : IRequestHandler<GetNoteQuery, GetNoteVM>
    {
        private readonly SqlContext _context;

        public GetNoteQueryHandler(SqlContext context)
        {
            _context = context;
        }

        public async Task<GetNoteVM> Handle(GetNoteQuery request, CancellationToken cancellationToken)
        {
            return await _context.Set<Note>()
                                 .Where(e => e.Id == request.Id)
                                 .Select(e => new GetNoteVM
                                 {
                                     Id = e.Id,
                                     Title = e.Title,
                                     Comments = e.Comments,
                                     Attach = e.Attach,
                                     Alert = e.Alert
                                 })
                                 .FirstOrDefaultAsync();
        }
    }
}