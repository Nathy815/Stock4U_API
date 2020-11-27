using S4U.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Domain.Entities
{
    public class UserEquityPrice : Base
    {
        public double Price { get; set; }
        public ePriceType Type { get; set; }

        // Relational
        public Guid EquityID { get; set; }
        public Guid UserID { get; set; }
        public virtual UserEquity UserEquity { get; set; }
    }
}
