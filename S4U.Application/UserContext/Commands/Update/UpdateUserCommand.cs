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
        public string ZipCode { get; set; }
        public string Local { get; set; }
        public string Number { get; set; }
        public string Compliment { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}