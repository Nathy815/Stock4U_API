using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using S4U.Application.UserContext.Commands.Create;
using S4U.Application.UserContext.Commands.Delete;
using S4U.Application.UserContext.Commands.Notify;
using S4U.Application.UserContext.Commands.Update;
using S4U.Application.UserContext.Queries;
using S4U.Domain.ViewModels;

namespace S4U.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        public UserController(IMediator mediator) : base(mediator) { }

        [HttpPost("create")]
        public async Task<Guid> Create([FromBody] CreateUserCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpDelete("delete/{id}")]
        public async Task<bool> Delete([FromRoute] Guid id)
        {
            return await _mediator.Send(new DeleteUserCommand(id));
        }

        [HttpGet("{userID}")]
        [Authorize]
        public async Task<GetUserVM> Get([FromRoute] Guid userID)
        {
            return await _mediator.Send(new GetUserQuery(userID));
        }

        [HttpGet("address/{zipCode}")]
        [Authorize]
        public async Task<GetAddressVM> GetAddress([FromRoute] string zipCode)
        {
            return await _mediator.Send(new GetAddressQuery(zipCode));
        }

        [HttpPost("findByEmail")]
        public async Task<Guid> FindByEmail([FromBody] FindByEmailQuery request)
        {
            return await _mediator.Send(request);
        }

        [HttpPatch("update"), DisableRequestSizeLimit]
        [Authorize]
        public async Task<bool> Update([FromForm] UpdateUserCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost("notify")]
        public async Task<bool> Notify([FromBody] NotifyUserCommand notify)
        {
            return await _mediator.Send(notify);
        }
    }
}