using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Domain.ViewModels
{
    public class GetEquityChartVM
    {
        public string Legend { get; set; }
        public Dictionary<DateTime, double?> Points { get; set; }

        public GetEquityChartVM()
        {
            Points = new Dictionary<DateTime, double?>();
        }
    }
}