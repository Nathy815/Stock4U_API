using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using S4U.Application.PriceContext.Commands.Create;
using S4U.Application.PriceContext.Commands.Delete;
using S4U.Application.PriceContext.Commands.Update;
using S4U.Application.PriceContext.Queries;
using S4U.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S4U.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PriceController : BaseController
    {
        public PriceController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<bool> Create([FromBody] CreatePriceCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete([FromRoute] Guid id)
        {
            return await _mediator.Send(new DeletePriceCommand(id));
        }

        [HttpGet("{id}")]
        public async Task<GetPriceVM> Get([FromRoute] Guid id)
        {
            return await _mediator.Send(new GetPriceQuery(id));
        }

        [HttpGet("{userID}/{equityID}")]
        public async Task<List<GetPriceVM>> List([FromRoute] Guid userID, Guid equityID)
        {
            return await _mediator.Send(new ListPricesQuery(userID, equityID));
        }

        [HttpPatch]
        public async Task<bool> Update([FromBody] UpdatePriceCommand request)
        {
            return await _mediator.Send(request);
        }
    }
}
