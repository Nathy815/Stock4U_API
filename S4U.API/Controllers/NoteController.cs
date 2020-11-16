using MediatR;
using Microsoft.AspNetCore.Mvc;
using S4U.Application.NoteContext.Commands.Create;
using S4U.Application.NoteContext.Commands.Delete;
using S4U.Application.NoteContext.Commands.Update;
using S4U.Application.NoteContext.Queries;
using S4U.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S4U.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : BaseController
    {
        public NoteController(IMediator mediator) : base(mediator) { }

        [HttpPost("create"), DisableRequestSizeLimit]
        public async Task<Guid> Create([FromForm] CreateNoteCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpDelete("delete/{id}")]
        public async Task<bool> Delete([FromRoute] Guid id)
        {
            return await _mediator.Send(new DeleteNoteCommand(id));
        }

        [HttpGet("{id}")]
        public async Task<GetNoteVM> Get([FromRoute] Guid id)
        {
            return await _mediator.Send(new GetNoteQuery(id));
        }

        [HttpPost("list")]
        public async Task<List<GetNoteVM>> List([FromBody] ListNotesQuery request)
        {
            return await _mediator.Send(request);
        }

        [HttpPatch("update"), DisableRequestSizeLimit]
        public async Task<bool> Update([FromForm] UpdateNoteCommand request)
        {
            return await _mediator.Send(request);
        }
    }
}