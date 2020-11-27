using S4U.Domain.Entities;
using S4U.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Domain.ViewModels
{
    public class GetPriceVM
    {
        public Guid Id { get; set; }
        public double Price { get; set; }
        public ePriceType Type { get; set; }
    }
}