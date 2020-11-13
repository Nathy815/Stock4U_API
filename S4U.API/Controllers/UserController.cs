using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using S4U.Application.UserContext.Create;

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
    }
}
