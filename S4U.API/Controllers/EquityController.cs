using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using S4U.Application.EquityContext.Commands.Create;
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

        [HttpPost("create")]
        [Authorize]
        public async Task<Guid> Create([FromBody] CreateEquityCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet("list/{userID}")]
        [Authorize]
        public async Task<List<GetEquityVM>> List([FromRoute] Guid userID)
        {
            return await _mediator.Send(new ListEquitiesQuery(userID));
        }

        [HttpGet("search/{term}")]
        [Authorize]
        public async Task<List<SearchEquityVM>> Search([FromRoute] string term)
        {
            return await _mediator.Send(new SearchEquityQuery(term));
        }
    }
}
