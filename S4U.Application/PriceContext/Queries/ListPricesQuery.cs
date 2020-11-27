using MediatR;
using S4U.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.PriceContext.Queries
{
    public class ListPricesQuery : IRequest<List<GetPriceVM>>
    {
        public Guid UserID { get; set; }
        public Guid EquityID { get; set; }

        public ListPricesQuery(Guid userID, Guid equityID)
        {
            UserID = userID;
            EquityID = equityID;
        }
    }
}