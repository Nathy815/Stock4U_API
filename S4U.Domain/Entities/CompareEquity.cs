using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace S4U.Domain.Entities
{
    public class CompareEquity
    {
        [Key]
        public Guid UserID { get; set; }
        public Guid EquityID { get; set; }
        public virtual UserEquity UserEquity { get; set; }

        [Key]
        public Guid CompareID { get; set; }
        public virtual Equity Equity { get; set; }
    }
}