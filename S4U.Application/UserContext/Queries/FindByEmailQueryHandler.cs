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

namespace S4U.Application.UserContext.Queries
{
    public class FindByEmailQueryHandler : IRequestHandler<FindByEmailQuery, Guid>
    {
        private readonly SqlContext _context;

        public FindByEmailQueryHandler(SqlContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(FindByEmailQuery request, CancellationToken cancellationToken)
        {
            var _user = await _context.Set<User>()
                                      .Where(u => u.Email.Equals(request.Email))
                                      .FirstOrDefaultAsync();

            return _user.Id;
        }
    }
}