using Newtonsoft.Json;
using S4U.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace S4U.Application.Utils
{
    public class ChartHelper
    {
        protected static List<DateTime> _holidays = GetHolidays();

        private static List<DateTime> GetHolidays()
        {
            var _today = DateTime.Now;
            var year = DateTime.Now.Year;
            var holidays = new List<DateTime>();
            var _newYear = new DateTime().AddYears(_today.Year - 1);
            holidays.Add(_newYear); // Ano Novo
            holidays.Add(_newYear.AddMonths(3).AddDays(20)); // 21/Abr = Tiradentes
            holidays.Add(_newYear.AddMonths(4)); // 01/Mai = Dia do Trabalho
            holidays.Add(_newYear.AddMonths(6).AddDays(9)); // 09/Jul = Dia da Revolução Constitucionalista
            holidays.Add(_newYear.AddMonths(8).AddDays(6)); // 07/Set = Dia da Independência
            holidays.Add(_newYear.AddMonths(9).AddDays(11)); // 12/Out = Dia da Nossa Senhora Aparecida
            holidays.Add(_newYear.AddMonths(10).AddDays(1)); // 02/Nov = Dia de Finados
            holidays.Add(_newYear.AddMonths(10).AddDays(19)); // 20/Nov = Dia da Consciência Negra
            holidays.Add(_newYear.AddMonths(11).AddDays(23)); // 24/Dez = Véspera de Natal
            holidays.Add(_newYear.AddMonths(11).AddDays(24)); // 25/Dez = Natal
            holidays.Add(_newYear.AddMonths(11).AddDays(30)); // 31/Dez = Véspera de Ano Novo

            var _pascoa = _newYear.AddMonths(3);
            var _count = 0;
            while (_count < 2)
            {
                if (_pascoa.DayOfWeek == DayOfWeek.Sunday)
                    _count += 1;

                if (_count != 2)
                    _pascoa = _pascoa.AddDays(1);
            }

            holidays.Add(_pascoa.AddDays(60)); // Corpus Christi
            holidays.Add(_pascoa.AddDays(-2)); // Sexta-Feira Santa
            holidays.Add(_pascoa); // Páscoa

            var _quartaCinzas = _pascoa;
            _count = 39;
            while (_count > 0)
            {
                if (_quartaCinzas.DayOfWeek != DayOfWeek.Sunday)
                    _count -= 1;

                _quartaCinzas = _quartaCinzas.AddDays(-1);
            }

            holidays.Add(_quartaCinzas); // Quarta-Feira de Cinzas
            holidays.Add(_quartaCinzas.AddDays(-1)); // Carnaval
            holidays.Add(_quartaCinzas.AddDays(-2)); // Emenda Carnaval

            return holidays;
        }

        private bool IsHoliday(DateTime date)
        {
            if (_holidays.Contains(date.Date))
                return true;

            return false;
        }

        private DateTime LastDayOfMonth(DateTime date)
        {
            var _firstDay = FirstDayOfMonth(date);
            var _lastDay = _firstDay.AddMonths(1).AddDays(-1);

            return _lastDay.Date.AddHours(10);
        }

        private DateTime FirstDayOfMonth(DateTime date)
        {
            return date.AddDays((date.Day - 1) * -1).Date.AddHours(10);
        }

        protected async Task<Dictionary<string, YahooVM>> GetData(string prefix, string range, string interval)
        {
            try
            {
                var _return = new Dictionary<string, YahooVM>();

                var api = string.Format("https://query1.finance.yahoo.com/v8/finance/chart/{0}?region=US&lang=en-US&includePrePost=false&interval={1}&range={2}&corsDomain=finance.yahoo.com&.tsrc=finance",
                                        prefix, interval, range);

                var _client = new HttpClient();
                var _response = await _client.GetAsync(api);
                var _json = _response.Content.ReadAsStringAsync().Result;

                var _data = JsonConvert.DeserializeObject<YahooVM>(_json);

                if (_data.chart.error != null)
                    throw new Exception("It was not possible to get Yahoo Finance data.");

                _return.Add(prefix, _data);

                return _return;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        protected DateTime GetLegenda(int count, string period, DateTime today, DayOfWeek dayOfWeek)
        {
            if (period.Equals("week"))
            {
                if (count == 0)
                {
                    var _days = 4;
                    while (_days > 0)
                    {
                        today = today.AddDays(-1);
                        while (IsHoliday(today) || today.DayOfWeek == DayOfWeek.Saturday || today.DayOfWeek == DayOfWeek.Sunday)
                            today = today.AddDays(-1);
                        _days--;
                    }
                }
                else
                {
                    today = today.AddDays(1);
                    while (IsHoliday(today) || today.DayOfWeek == DayOfWeek.Saturday || today.DayOfWeek == DayOfWeek.Sunday)
                        today = today.AddDays(1);
                }
            }
            else if (period.Equals("month"))
            {
                if (count == 0)
                {
                    today = today.AddMonths(-1);
                    while (IsHoliday(today) || today.DayOfWeek == DayOfWeek.Saturday || today.DayOfWeek == DayOfWeek.Sunday)
                        today = today.AddDays(1);
                }
                else
                {
                    today = today.AddDays(7);
                    while (today.DayOfWeek != dayOfWeek)
                        today = today.AddDays(-1);
                    while (IsHoliday(today) || today.DayOfWeek == DayOfWeek.Saturday || today.DayOfWeek == DayOfWeek.Sunday)
                        today = today.AddDays(1);
                }
            }
            else if (period.Equals("threeMonths") || period.Equals("sixMonths"))
            {
                if (count == 0)
                {
                    today = period.Equals("threeMonths") ? today.AddMonths(-3) : today.AddMonths(-6);
                    while (IsHoliday(today) || today.DayOfWeek == DayOfWeek.Saturday || today.DayOfWeek == DayOfWeek.Sunday)
                        today = today.AddDays(1);
                }
                else
                {
                    today = LastDayOfMonth(today).AddDays(1);
                    while (IsHoliday(today) || today.DayOfWeek == DayOfWeek.Saturday || today.DayOfWeek == DayOfWeek.Sunday)
                        today = today.AddDays(1);
                }
            }
            else if (period.Equals("year"))
            {
                if (count == 0)
                {
                    today = today.AddYears(-1);
                    while (IsHoliday(today) || today.DayOfWeek == DayOfWeek.Saturday || today.DayOfWeek == DayOfWeek.Sunday)
                        today = today.AddDays(1);
                }
                else
                {
                    today = today.AddMonths(3);
                    today = FirstDayOfMonth(today).AddDays(1);
                    while (IsHoliday(today) || today.DayOfWeek == DayOfWeek.Saturday || today.DayOfWeek == DayOfWeek.Sunday)
                        today = today.AddDays(1);
                    if (today > DateTime.Now.Date)
                        today = DateTime.Now.Date;
                }
            }
            else
            {
                if (count == 0)
                {
                    today = today.AddYears(-5);
                    if (today.Day > 1)
                    {
                        today = today.AddMonths(1);
                        today = FirstDayOfMonth(today);
                    }
                }
                else
                {
                    today = new DateTime(today.Year + 1, 1, 1);
                    if (today > DateTime.Now.Date)
                        today = DateTime.Now.Date;
                }
            }

            return today;
        }

        protected string GetRange(string period)
        {
            switch (period)
            {
                case "day":
                    return "1d";
                case "week":
                    return "5d";
                case "month":
                    return "1mo";
                case "threeMonths":
                    return "3mo";
                case "sixMonths":
                    return "6mo";
                case "year":
                    return "1y";
                case "fiveYears":
                    return "5y";
                default:
                    throw new Exception("Invalid period!");
            }
        }

        protected string GetInterval(string period)
        {
            switch (period)
            {
                case "day":
                    return "2m";
                case "week":
                    return "15m";
                case "month":
                    return "1d";
                case "threeMonths":
                    return "1d";
                case "sixMonths":
                    return "1d";
                case "year":
                    return "1wk";
                case "fiveYears":
                    return "1mo";
                default:
                    throw new Exception("Invalid period.");
            }
        }

        protected int InvalidDays(DateTime start, DateTime end)
        {
            var _days = 0;

            while (start.Date != end.Date)
            {
                if (start.DayOfWeek == DayOfWeek.Saturday ||
                    start.DayOfWeek == DayOfWeek.Sunday ||
                    IsHoliday(start))
                    _days++;

                start = start.AddDays(1);
            }

            return _days;
        }

        protected bool IsBovespaOnly(string prefix)
        {
            if (prefix.Contains(".SA"))
                return true;
            return false;
        }
    }
}
