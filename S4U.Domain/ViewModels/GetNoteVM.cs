using S4U.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Domain.ViewModels
{
    public class GetNoteVM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Comments { get; set; }
        public string Attach { get; set; }
        public DateTime? Alert { get; set; }
    }
}