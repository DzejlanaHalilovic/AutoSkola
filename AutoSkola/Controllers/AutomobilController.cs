using AutoSkola.Contracts.Models.Automobil.Request;
using AutoSkola.Mediator.Automobil;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoSkola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutomobilController : ControllerBase
    {
        private readonly IMediator mediator;

        public AutomobilController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await mediator.Send(new GetAllAutoQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            var result = await mediator.Send(new GetAutoQuery(id));
            if (!result.IsSuccess)
                return NotFound(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult>CreateAutomobil(CreateAutomobilRequest request)
        {
            var result = await mediator.Send(new CreateAutoCommand(request));
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteAuto(int id)
        {
            var result =await mediator.Send(new RemoveAutoCommand(id));
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.IsSuccess);
        }

        [HttpPut]
        public async Task<IActionResult>UpdateAuto(int id, CreateAutomobilRequest request)
        {
            var result = await mediator.Send(new UpdateAutoCommand(id, request));
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }
    }
}
