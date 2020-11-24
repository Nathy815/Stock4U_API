using MediatR;
using S4U.Application.Utils;
using S4U.Domain.Enums;
using S4U.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace S4U.Application.EquityContext.Queries
{
    public class GenerateChartQueryHandler : ChartHelper, IRequestHandler<GenerateChartQuery, List<GetEquityChartVM>>
    {
        public async Task<List<GetEquityChartVM>> Handle(GenerateChartQuery request, CancellationToken cancellationToken)
        {
            var _result = new List<GetEquityChartVM>();

            // Setup
            var _range = GetRange(request.Filter);
            var _interval = GetInterval(request.Filter);
            var _data = await GetData(request.Ticker, _range, _interval);
            var _prefix = _data.First().Key;
            var _registros = _data.First().Value.chart.result[0].indicators.quote[0].close;
            var _isBovespa = IsBovespaOnly(request.Ticker);
            var _today = DateTime.Now;
            var _start = GetLegenda(0, request.Filter, _today, DayOfWeek.Monday); // auxiliar para filtro mensal
            var _dayWeek = _start.DayOfWeek;

            for (var i = 0; i < _registros.Count; i++)
            {
                switch (request.Filter)
                {
                    case "day":
                        _result.Add(new GetEquityChartVM(_start.AddMinutes(2 * (_result.Count + 1)), _registros.ElementAt(i)));

                        break;
                    case "week":
                        _result.Add(new GetEquityChartVM(_start.AddMinutes(15 * (_result.Count + 1)), _registros.ElementAt(i)));

                        break;
                    case "year":
                        _result.Add(new GetEquityChartVM(_start.AddDays(7 * (_result.Count + 1)), _registros.ElementAt(i)));

                        break;
                    case "fiveYears":
                        _result.Add(new GetEquityChartVM(_start.AddMonths(1 * (_result.Count + 1)), _registros.ElementAt(i)));

                        break;
                    default:
                        _result.Add(new GetEquityChartVM(_start.AddDays(1 * (_result.Count + 1)), _registros.ElementAt(i)));

                        break;
                }
            }

            return _result;
        }
    }
}
