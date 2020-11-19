using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Domain.Entities
{
    public class Notification : Base
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public Guid? RedirectID { get; set; }

        // Relational
        public Guid UserID { get; set; }
        public virtual User User { get; set; }
    }
}