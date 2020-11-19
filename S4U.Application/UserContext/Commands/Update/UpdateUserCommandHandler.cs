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

namespace S4U.Application.UserContext.Commands.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly SqlContext _context;

        public UpdateUserCommandHandler(SqlContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var _user = await _context.Set<User>()
                                      .Where(u => u.Id == request.Id)
                                      .FirstOrDefaultAsync();

            _user.Gender = request.Gender;
            _user.Image = null;
            _user.BirthDate = request.BirthDate;
            _user.Address = request.Address;
            _user.Number = request.Number;
            _user.Compliment = request.Compliment;

            _context.Users.Update(_user);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
