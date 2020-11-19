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

namespace S4U.Application.UserContext.Queries
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserVM>
    {
        private readonly SqlContext _context;

        public GetUserQueryHandler(SqlContext context)
        {
            _context = context;
        }

        public async Task<GetUserVM> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var _user = await _context.Set<User>()
                                      .Where(u => u.Id == request.UserID)
                                      .FirstOrDefaultAsync();

            return new GetUserVM(_user);
        }
    }
}