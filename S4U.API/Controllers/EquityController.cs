using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using S4U.Application.EquityContext.Commands.Compare;
using S4U.Application.EquityContext.Commands.Create;
using S4U.Application.EquityContext.Commands.Delete;
using S4U.Application.EquityContext.Commands.Remove;
using S4U.Application.EquityContext.Queries;
using S4U.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S4U.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquityController : BaseController
    {
        public EquityController(IMediator mediator) : base(mediator) { }

        [HttpPost("compare")]
        public async Task<bool> Compare([FromBody] CompareEquityCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost("chart")]
        public async Task<List<ListEquityChartsVM>> Chart([FromBody] GetEquityChartQuery request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost("create")]
        public async Task<Guid> Create([FromBody] CreateEquityCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpDelete("delete/{equityID}/{userID}")]
        public async Task<bool> Delete([FromRoute] Guid equityID, Guid userID)
        {
            return await _mediator.Send(new DeleteUserEquityCommand(userID, equityID));
        }

        [HttpPost("get")]
        public async Task<GetEquityVM> Get([FromBody] GetEquityQuery request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet("list/{userID}")]
        public async Task<List<GetEquityVM>> List([FromRoute] Guid userID)
        {
            return await _mediator.Send(new ListEquitiesQuery(userID));
        }

        [HttpPatch("remove")]
        public async Task<bool> Remove([FromBody] RemoveCompareCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet("search/{term}")]
        public async Task<List<SearchEquityVM>> Search([FromRoute] string term)
        {
            return await _mediator.Send(new SearchEquityQuery(term));
        }
    }
}
