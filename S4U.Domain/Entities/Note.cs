using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Domain.Entities
{
    public class Note : Base
    {
        public string Title { get; set; }
        public string Comments { get; set; }
        public string Attach { get; set; }
        public DateTime? Alert { get; set; }

        //Relational
        public Guid EquityID { get; set; }
        public Guid UserID { get; set; }
        public virtual UserEquity UserEquity { get; set; }
    }
}