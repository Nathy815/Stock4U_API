using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Domain.ViewModels
{
    public class SearchEquityVM
    {
        public string Ticker { get; set; }
        public string Name { get; set; }

        public SearchEquityVM(QuoteViewModel item)
        {
            Ticker = item.symbol;
            Name = item.longname;
        }
    }
}