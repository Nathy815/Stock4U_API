using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Domain.ViewModels
{
    public class ListEquityChartsVM
    {
        public string Ticker { get; set; }
        public List<GetEquityChartVM> Chart { get; set; }

        public ListEquityChartsVM(string ticker, List<GetEquityChartVM> chart)
        {
            Ticker = ticker;
            Chart = chart;
        }
    }
}