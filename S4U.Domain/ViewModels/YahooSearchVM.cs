using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Domain.ViewModels
{
    public class YahooSearchVM
    {
        public List<QuoteViewModel> quotes { get; set; }
    }

    public class QuoteViewModel
    {
        public string longname { get; set; }
        public string symbol { get; set; }
        public bool isYahooFinance { get; set; }
    }
}