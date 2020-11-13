using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace S4U.Domain.Entities
{
    public class Plan
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Descyption { get; set; }
        public double Price { get; set; }
        
        // Relational
        public ICollection<Payment> Payments { get; set; }
    }
}