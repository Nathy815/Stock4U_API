using MediatR;
using Newtonsoft.Json;
using S4U.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace S4U.Application.EquityContext.Queries
{
    public class GetEquityValueQueryHandler : IRequestHandler<GetEquityValueQuery, List<GetEquityItemVM>>
    {
        public async Task<List<GetEquityItemVM>> Handle(GetEquityValueQuery request, CancellationToken cancellationToken)
        {
            var api = string.Format("https://query1.finance.yahoo.com/v8/finance/chart/{0}?symbol={0}&range=5d&interval=1d", request.Ticker);

            var _client = new HttpClient();
            var _response = await _client.GetAsync(api);
            var _json = _response.Content.ReadAsStringAsync().Result;

            var _return = JsonConvert.DeserializeObject<YahooVM>(_json);
            var _list = new List<GetEquityItemVM>();

            var _values = _return.chart.result[0].indicators.quote[0];
            _list.Add(new GetEquityItemVM("Abertura", _values.open[_values.open.Count - 1], _values.open.Count > 1 ? _values.open[_values.open.Count - 2] : _values.open[_values.open.Count - 1]));
            _list.Add(new GetEquityItemVM("Mínimo", _values.low[_values.low.Count - 1], _values.low.Count > 1 ? _values.low[_values.low.Count - 2] : _values.low[_values.low.Count - 1]));
            _list.Add(new GetEquityItemVM("Máximo", _values.high[_values.high.Count - 1], _values.high.Count > 1 ? _values.high[_values.high.Count - 2] : _values.high[_values.high.Count - 1]));
            _list.Add(new GetEquityItemVM("Fechamento", _values.close[_values.close.Count - 1], _values.close.Count > 1 ? _values.close[_values.close.Count - 2] : _values.close[_values.close.Count - 1]));
            _list.Add(new GetEquityItemVM("Volume", _values.volume[_values.volume.Count - 1], _values.volume.Count > 1 ? _values.volume[_values.volume.Count - 2] : _values.volume[_values.volume.Count - 1]));

            return _list;
        }
    }
}