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
        public string ZipCode { get; set; }
        public string Local { get; set; }
        public string Number { get; set; }
        public string Compliment { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public GetUserVM(User user)
        {
            Id = user.Id;
            Nome = user.Name;
            Email = user.Email;
            Gender = user.Gender;
            Image = user.Image;
            BirthDate = user.BirthDate;
            if (user.AddressID.HasValue)
            {
                ZipCode = user.Address.ZipCode;
                Local = user.Address.Local;
                Number = user.Address.Number;
                Compliment = user.Address.Compliment;
                Neighborhood = user.Address.Neighborhood;
                City = user.Address.City;
                State = user.Address.State;
            }
        }
    }
}
