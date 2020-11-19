using System;
using System.Collections.Generic;

namespace S4U.Domain.Entities
{
    public class User : Base
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string Compliment { get; set; }
        public string PushToken { get; set; }
        
        // Optional
        public string Gender { get; set; }
        public string Image { get; set; }
        public DateTime? BirthDate { get; set; }

        // Relational
        public Guid RoleID { get; set; }
        public virtual Role Role { get; set; }

        public Guid? SignatureID { get; set; }
        public virtual Signature Signature { get; set; }

        public virtual ICollection<UserEquity> UsersEquities { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}