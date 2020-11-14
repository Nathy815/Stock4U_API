using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Domain.Entities
{
    public class Address : Base
    {
        public string ZipCode { get; set; }
        public string Local { get; set; }
        public string Number { get; set; }
        public string Compliment { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        // Relational
        public virtual ICollection<User> Users { get; set; }
    }
}
