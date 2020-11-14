using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Domain.ViewModels
{
    public class GetAddressVM
    {
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string bairro { get; set; }
        public string localidade { get; set; }
        public string uf { get; set; }
    }
}