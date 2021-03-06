﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.UserContext.Commands.Create
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string PushToken { get; set; }
    }
}