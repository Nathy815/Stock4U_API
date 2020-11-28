using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace S4U.Domain.ViewModels
{
    public class YahooVM
    {
        [Key]
        public Guid Id { get; set; }
        public Guid dealID { get; set; }
        public string Range { get; set; }
        public YahooChartViewModel chart { get; set; }
    }

    public class YahooChartViewModel
    {
        public List<YahooResultViewModel> result { get; set; }
        public YahooErrorViewModel error { get; set; }
    }

    public class YahooErrorViewModel
    {
        public string code { get; set; }
        public string description { get; set; }
    }

    public class YahooResultViewModel
    {
        public YahooMetaViewModel meta { get; set; }
        public List<int> timestamp { get; set; }
        public YahooIndicatorsViewModel indicators { get; set; }
    }

    public class YahooMetaViewModel
    {
        public string currency { get; set; }
        public string symbol { get; set; }
        public string range { get; set; }
        public string dataGranularity { get; set; }
        public double regularMarketPrice { get; set; }
        public double chartPreviousClose { get; set; }
        public List<List<YahooPeriodViewModel>> tradingPeriods { get; set; }
    }

    public class YahooPeriodViewModel
    {
        public string timezone { get; set; }
        public int start { get; set; }
        public int end { get; set; }
        public double gmtoffset { get; set; }
    }

    public class YahooIndicatorsViewModel
    {
        public List<YahooIndicatorViewModel> quote { get; set; }
    }

    public class YahooIndicatorViewModel
    {
        public List<double?> close { get; set; }
        public List<double?> open { get; set; }
        public List<double?> high { get; set; }
        public List<double?> low { get; set; }
        public List<double?> volume { get; set; }
    }
}
