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
                                      .Include(u => u.Address)
                                      .Where(u => u.Id == request.Id)
                                      .FirstOrDefaultAsync();

            _user.Gender = request.Gender;
            _user.Image = null;
            _user.BirthDate = request.BirthDate;

            if (!string.IsNullOrEmpty(request.ZipCode))
            {
                var _address = await _context.Set<Address>()
                                             .Where(a => a.ZipCode.Equals(request.ZipCode))
                                             .FirstOrDefaultAsync();

                if (_address != null) _user.AddressID = _address.Id;
                else
                    _user.Address = new Address
                    {
                        Id = Guid.NewGuid(),
                        ZipCode = request.ZipCode,
                        Local = request.Local,
                        Number = request.Number,
                        Compliment = request.Compliment,
                        Neighborhood = request.Neighborhood,
                        City = request.City,
                        State = request.State
                    };
            }

            _context.Users.Update(_user);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
