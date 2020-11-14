using MediatR;
using Newtonsoft.Json;
using S4U.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace S4U.Application.EquityContext.Queries
{
    public class SearchEquityQueryHandler : IRequestHandler<SearchEquityQuery, List<SearchEquityVM>>
    {
        public async Task<List<SearchEquityVM>> Handle(SearchEquityQuery request, CancellationToken cancellationToken)
        {
            var _client = new HttpClient();
            var _response = await _client.GetAsync("https://query1.finance.yahoo.com/v1/finance/search?q=" + request.Term);
            var _json = _response.Content.ReadAsStringAsync().Result;

            var _data = JsonConvert.DeserializeObject<YahooSearchVM>(_json);
            var _result = _data.quotes.Where(r => r.isYahooFinance).ToList();

            var _lista = new List<SearchEquityVM>();
            foreach (var _item in _result)
                _lista.Add(new SearchEquityVM(_item));

            return _lista;
        }
    }
}
