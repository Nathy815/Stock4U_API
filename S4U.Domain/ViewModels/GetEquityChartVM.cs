using System;
using System.Collections.Generic;
using System.Text;

namespace S4U.Domain.ViewModels
{
    public class GetEquityChartVM
    {
        public DateTime Legend { get; set; }
        public double? Point { get; set; }

        public GetEquityChartVM() { }

        public GetEquityChartVM(DateTime legend, double? point)
        {
            Legend = legend;
            Point = point;
        }
    }
}