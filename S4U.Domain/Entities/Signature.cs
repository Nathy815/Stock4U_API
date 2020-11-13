using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Domain.Entities
{
    public class Signature : Base
    {
        public double Price { get; set; }
        public DateTime ExpiredDate { get; set; }

        // Relational
        public Guid PlanID { get; set; }
        public virtual Plan Plan { get; set; }

        public virtual User User { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}