using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Domain.Entities
{
    public class Equity : Base
    {
        public string Ticker { get; set; }
        public string Name { get; set; }

        // Relational
        public virtual ICollection<UserEquity> UsersEquities { get; set; }
        public virtual ICollection<CompareEquity> EquitiesThatCompare { get; set; }
    }
}
