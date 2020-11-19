using MediatR;
using Newtonsoft.Json;
using S4U.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace S4U.Application.UserContext.Queries
{
    public class GetAddressQueryHandler : IRequestHandler<GetAddressQuery, GetAddressVM>
    {
        public async Task<GetAddressVM> Handle(GetAddressQuery request, CancellationToken cancellationToken)
        {
            var api = string.Format("http://viacep.com.br/ws/{0}/json/", request.ZipCode.Replace("-", ""));

            var _client = new HttpClient();
            var _response = await _client.GetAsync(api);
            var _json = _response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<GetAddressVM>(_json);
        }
    }
}