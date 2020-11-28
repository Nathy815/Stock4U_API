using S4U.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace S4U.Domain.ViewModels
{
    public class GetEquityVM
    {
        public Guid Id { get; set; }
        public string Ticker { get; set; }
        public string Name { get; set; }
        public List<GetEquityItemVM> Items { get; set; }
        public List<GetEquityCompareVM> Compare { get; set; }

        public GetEquityVM()
        {
            Items = new List<GetEquityItemVM>();
            Compare = new List<GetEquityCompareVM>();
        }

        public GetEquityVM(Equity equity, List<GetEquityItemVM> yahoo)
        {
            Id = equity.Id;
            Ticker = equity.Ticker;
            Name = equity.Name;
            Items = yahoo;
        }
    }

    public class GetEquityItemVM
    {
        public string label { get; set; }
        public double value { get; set; }
        public double percentage { get; set; }
        public bool? higher { get; set; }

        public GetEquityItemVM(string nome, double? valorAnt, double? valor)
        {
            label = nome;
            value = Math.Round(valor.HasValue ? valor.Value : 0, 2);
            var _anterior = Math.Round(valorAnt.HasValue ? valorAnt.Value : 0, 2);
            percentage = Math.Round((_anterior - value) * 100 / _anterior, 2);
            higher = null;
            if (value != _anterior)
                higher = value > _anterior ? true : false;
        }
    }

    public class GetEquityCompareVM
    {
        public Guid Id { get; set; }
        public string Ticker { get; set; }
        public string Name { get; set; }
        public double value { get; set; }
        public double percentage { get; set; }
        public bool? higher { get; set; }

        public GetEquityCompareVM(Equity equity, List<GetEquityItemVM> yahoo)
        {
            Id = equity.Id;
            Ticker = equity.Ticker;
            Name = equity.Name;
            var _value = yahoo.ElementAt(3);
            value = _value.value;
            percentage = _value.percentage;
            higher = _value.higher;
        }
    }
}