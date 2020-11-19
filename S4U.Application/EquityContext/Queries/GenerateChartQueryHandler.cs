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
            var _item = new GetEquityChartVM();
            var _isBovespa = IsBovespaOnly(request.Ticker);
            var _today = DateTime.Now;
            var _start = GetLegenda(0, request.Filter, _today, DayOfWeek.Monday); // auxiliar para filtro mensal
            var _next = GetLegenda(1, request.Filter, _start, _start.DayOfWeek);
            var _dayWeek = _start.DayOfWeek;

            for (var i = 0; i < _registros.Count; i++)
            {
                switch (request.Filter)
                {
                    case "day":
                        if (i == 0 || i % 5 == 0 || i == _registros.Count - 1)
                            _item.Points.Add(DateTime.Now, _registros.ElementAt(i));

                        if (i == _registros.Count - 1 ||
                            (_result.Count == 0 && _item.Points.Count == 5) ||
                            _item.Points.Count == 6)
                        {
                            _item.Legend = _isBovespa ? (10 + _result.Count).ToString() : (9 + _result.Count).ToString();
                            _result.Add(_item);
                            _item = new GetEquityChartVM();
                        }

                        break;
                    case "week":
                        if ((_isBovespa && i % 2 != 0) ||
                            (!_isBovespa && i % 2 == 0) ||
                            i == _registros.Count - 1)
                            _item.Points.Add(DateTime.Now, _registros.ElementAt(i));

                        if (i == _registros.Count - 1 ||
                            (_item.Points.Count > 0 && _item.Points.Count % 14 == 0))
                        {
                            _today = GetLegenda(_result.Count, request.Filter, _today, _dayWeek);
                            _item.Legend = _today.Day.ToString();
                            _result.Add(_item);
                            _item = new GetEquityChartVM();
                        }

                        break;
                    case "month":
                        _item.Points.Add(DateTime.Now, _registros.ElementAt(i));

                        var days = (_next - _start).Days - InvalidDays(_start, _next);

                        if (i == _registros.Count - 1 ||
                            (_item.Points.Count > 0 && _item.Points.Count == days))
                        {
                            _item.Legend = _start.Day.ToString();
                            _result.Add(_item);
                            _item = new GetEquityChartVM();
                            _start = _next;
                            _next = GetLegenda(_result.Count + 1, request.Filter, _start, _dayWeek);
                        }

                        break;
                    case "year":
                        _item.Points.Add(DateTime.Now, _registros.ElementAt(i));

                        if (i == _registros.Count - 1 ||
                            (_item.Points.Count > 0 && _item.Points.Count == 12))
                        {
                            _item.Legend = Enum.GetName(typeof(eMonth), _start.Month);
                            _result.Add(_item);
                            _item = new GetEquityChartVM();
                            _start = _next;
                            _next = GetLegenda(_result.Count + 1, request.Filter, _start, _dayWeek);
                        }

                        break;
                    case "fiveYears":
                        _item.Points.Add(DateTime.Now, _registros.ElementAt(i));

                        var months = 12;
                        if (_start.Month != _next.Month)
                            months = (int)Math.Round((_next - _start).TotalDays / 30);
                        if (_next == DateTime.Now.Date)
                            months += 1;

                        if (i == _registros.Count - 1 ||
                            (_item.Points.Count > 0 && _item.Points.Count == months))
                        {
                            _item.Legend = _start.Year.ToString();
                            _result.Add(_item);
                            _item = new GetEquityChartVM();
                            _start = _next;
                            _next = GetLegenda(_result.Count + 1, request.Filter, _start, _dayWeek);
                        }

                        break;
                    default:
                        _item.Points.Add(DateTime.Now, _registros.ElementAt(i));

                        days = (int)Math.Round((_next - _start).TotalDays) - InvalidDays(_start, _next);

                        if (i == _registros.Count - 1 ||
                            (_item.Points.Count > 0 && _item.Points.Count == days))
                        {
                            _item.Legend = Enum.GetName(typeof(eMonth), _start.Month);
                            _result.Add(_item);
                            _item = new GetEquityChartVM();
                            _start = _next;
                            _next = GetLegenda(_result.Count + 1, request.Filter, _start, _dayWeek);
                        }

                        break;
                }
            }

            return _result;
        }
    }
}
