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
    public class GetEquityValueQueryHandler : IRequestHandler<GetEquityValueQuery, Tuple<double, bool?>>
    {
        public async Task<Tuple<double, bool?>> Handle(GetEquityValueQuery request, CancellationToken cancellationToken)
        {
            var api = string.Format("https://query1.finance.yahoo.com/v8/finance/chart/{0}?symbol={0}&range=1d&interval=1d", request.Ticker);

            var _client = new HttpClient();
            var _response = await _client.GetAsync(api);
            var _json = _response.Content.ReadAsStringAsync().Result;

            var _return = JsonConvert.DeserializeObject<YahooVM>(_json);

            var _valor = _return.chart.result[0].meta.regularMarketPrice;
            var _fechamento = _return.chart.result[0].meta.chartPreviousClose;
            bool? _aumento = null;
            _aumento = _valor > _fechamento ? true : false;

            return new Tuple<double, bool?>(_valor, _aumento);
        }
    }
}
