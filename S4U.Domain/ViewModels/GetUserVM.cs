using S4U.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Domain.ViewModels
{
    public class GetUserVM
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Image { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string Compliment { get; set; }

        public GetUserVM(User user)
        {
            Id = user.Id;
            Nome = user.Name;
            Email = user.Email;
            Gender = user.Gender;
            Image = user.Image;
            BirthDate = user.BirthDate;
            Address = user.Address;
            Number = user.Number;
            Compliment = user.Compliment;
        }
    }
}
