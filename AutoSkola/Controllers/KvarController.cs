using AutoSkola.Contracts.Models.Kvar.Request;
using AutoSkola.Mediator.Kvar;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoSkola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KvarController : ControllerBase
    {
        private readonly IMediator mediator;

        public KvarController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult>CreateKvar(CreateKvarRequest request)
        {
            var result = await mediator.Send(new CreateKvarCommand(request));
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await mediator.Send(new GetAllKvarQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            var result = await mediator.Send(new GetKvarQuery(id));
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteKvar(int id)
        {
            var result = await mediator.Send(new RemoveKvarCommand(id));
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>UpdateKvar(int id, CreateKvarRequest request)
        {
            var result = await mediator.Send(new UpdateKvarCommand(id, request));
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }
    }
}
