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
        public double Variation { get; set; }
        public double Percentage { get; set; }
        public bool? Higher { get; set; }
        public int Notes { get; set; }
        public List<GetEquityVM> Compare { get; set; }

        public GetEquityVM(UserEquity userEquity, Tuple<double, double> yahoo)
        {
            Id = userEquity.Equity.Id;
            Ticker = userEquity.Equity.Ticker;
            Name = userEquity.Equity.Name;
            Value = yahoo.Item1;
            Higher = yahoo.Item1 > yahoo.Item2 ? true : false;
            if (yahoo.Item1 == yahoo.Item2) Higher = null;
            Variation = Math.Round(yahoo.Item1 - yahoo.Item2, 2);
            Percentage = Math.Round(Variation * 100 / Value, 2);
            Notes = userEquity.Notes == null ? 0 : userEquity.Notes.Count;
            Compare = new List<GetEquityVM>();
        }

        public GetEquityVM(Equity equity, Tuple<double, double> yahoo)
        {
            Id = equity.Id;
            Ticker = equity.Ticker;
            Name = equity.Name;
            Value = yahoo.Item1;
            Higher = yahoo.Item1 > yahoo.Item2 ? true : false;
            if (yahoo.Item1 == yahoo.Item2) Higher = null;
            Variation = Math.Round(yahoo.Item1 - yahoo.Item2, 2);
            Percentage = Math.Round(Variation * 100 / Value, 2);
        }
    }
}