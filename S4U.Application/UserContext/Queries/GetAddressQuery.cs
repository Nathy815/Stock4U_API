using MediatR;
using S4U.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Application.UserContext.Queries
{
    public class GetAddressQuery : IRequest<GetAddressVM>
    {
        public string ZipCode { get; set; }

        public GetAddressQuery(string zipCode)
        {
            ZipCode = zipCode;
        }
    }
}