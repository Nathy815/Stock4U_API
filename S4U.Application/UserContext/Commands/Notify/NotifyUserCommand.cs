﻿using MediatR;
using S4U.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.UserContext.Commands.Notify
{
    public class NotifyUserCommand : IRequest<bool>
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public Guid? RedirectID { get; set; }
        public eRedirectType RedirectType { get; set; }
        public Guid UserID { get; set; }
    }
}