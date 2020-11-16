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
    public class ListNotesQueryHandler : IRequestHandler<ListNotesQuery, List<GetNoteVM>>
    {
        private readonly SqlContext _context;

        public ListNotesQueryHandler(SqlContext context)
        {
            _context = context;
        }

        public async Task<List<GetNoteVM>> Handle(ListNotesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Set<Note>()
                                 .Where(e => !e.Deleted &&
                                             e.UserID == request.UserID &&
                                             e.EquityID == request.EquityID)
                                 .Select(e => new GetNoteVM
                                 {
                                     Id = e.Id,
                                     Title = e.Title,
                                     Comments = e.Comments,
                                     Attach = e.Attach,
                                     Alert = e.Alert
                                 })
                                 .ToListAsync();
        }
    }
}
