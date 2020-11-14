using MediatR;
using S4U.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.UserContext.Queries
{
    public class GetUserQuery : IRequest<GetUserVM>
    {
        public Guid UserID { get; set; }

        public GetUserQuery(Guid userID)
        {
            UserID = userID;
        }
    }
}