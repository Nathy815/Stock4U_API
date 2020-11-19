using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.UserContext.Commands.Update
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Gender { get; set; }
        public IFormFile Image { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string Compliment { get; set; }
    }
}