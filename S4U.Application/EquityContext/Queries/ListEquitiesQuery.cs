using MediatR;
using S4U.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.EquityContext.Queries
{
    public class ListEquitiesQuery : IRequest<List<GetEquityVM>>
    {
        public Guid UserID { get; set; }
        
        public ListEquitiesQuery(Guid userID)
        {
            UserID = userID;
        }
    }
}