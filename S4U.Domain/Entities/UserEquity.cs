using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace S4U.Domain.Entities
{
    public class UserEquity
    {
        // Relational
        [Key]
        public Guid UserID { get; set; }
        public virtual User User { get; set; }

        [Key]
        public Guid EquityID { get; set; }
        public virtual Equity Equity { get; set; }

        public virtual ICollection<Note> Notes { get; set; }
    }
}