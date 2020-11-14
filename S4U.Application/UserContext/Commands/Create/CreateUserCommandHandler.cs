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

namespace S4U.Application.UserContext.Commands.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly SqlContext _context;

        public CreateUserCommandHandler(SqlContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var _user = await _context.Set<User>()
                                      .Where(u => u.Email.Equals(request.Email))
                                      .FirstOrDefaultAsync();

            if (_user == null)
            {
                var _id = Guid.NewGuid();

                var _role = await _context.Set<Role>()
                                          .Where(r => r.Name.Equals(string.IsNullOrEmpty(request.Role) ? "Client" : request.Role))
                                          .Select(r => r.Id)
                                          .FirstOrDefaultAsync();

                await _context.Users.AddAsync(new User
                {
                    Id = _id,
                    Name = request.Name,
                    Email = request.Email,
                    RoleID = _role
                }, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                return _id;
            }

            return _user.Id;
        }
    }
}