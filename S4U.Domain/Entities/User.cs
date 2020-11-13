﻿using System;

namespace S4U.Domain.Entities
{
    public class User : Base
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PushToken { get; set; }
        
        // Optional
        public string Gender { get; set; }

        // Relational
        public Guid RoleID { get; set; }
        public virtual Role Role { get; set; }

        public Guid? SignatureID { get; set; }
        public virtual Signature Signature { get; set; }

        public Guid? AddressID { get; set; }
        public virtual Address Address { get; set; }
    }
}