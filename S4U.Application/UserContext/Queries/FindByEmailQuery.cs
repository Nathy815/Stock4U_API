using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.UserContext.Queries
{
    public class FindByEmailQuery : IRequest<Guid>
    {
        public string Email { get; set; }
        public string PushToken { get; set; }
    }
}