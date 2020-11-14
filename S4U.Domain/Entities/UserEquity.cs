using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Domain.Entities
{
    public class UserEquity
    {
        // Relational
        public Guid UserID { get; set; }
        public virtual User User { get; set; }

        public Guid EquityID { get; set; }
        public virtual Equity Equity { get; set; }

        //public virtual ICollection<Equity> EquitiesToCompare { get; set; }
    }
}