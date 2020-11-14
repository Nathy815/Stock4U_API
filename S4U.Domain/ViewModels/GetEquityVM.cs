using S4U.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Domain.ViewModels
{
    public class GetEquityVM
    {
        public Guid Id { get; set; }
        public string Ticker { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public bool? Higher { get; set; }

        public GetEquityVM(Equity equity, Tuple<double, bool?> yahoo)
        {
            Id = equity.Id;
            Ticker = equity.Ticker;
            Name = equity.Ticker;
            Value = yahoo.Item1;
            Higher = yahoo.Item2;
        }
    }
}