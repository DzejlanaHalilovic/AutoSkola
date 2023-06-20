using AutoSkola.Contracts.Models.Raspored.Request;
using AutoSkola.Data;
using AutoSkola.Infrastructure;
using AutoSkola.Mediator.Raspored;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoSkola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RasporedController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IUnitOfWork unitOfWork;
        private readonly DataContext context;

        public RasporedController(IMediator mediator, IUnitOfWork unitOfWork, DataContext context)
        {
            this.mediator = mediator;
            this.unitOfWork = unitOfWork;
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await mediator.Send(new GetAllRasporedQuery()));

        [HttpPost]
        public async Task<IActionResult> createRaspored( CreateRasporedRequest request)
        {
            var result = await mediator.Send(new CreateRasporedCommand(request));
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            var result = await mediator.Send(new GetOneRasporedQuery(id));
            if (!result.IsSuccess)
                return NotFound(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }

        [HttpPut]

        public async Task<IActionResult>updateRaspored(int id, UpdateRasporedRequest request)
        {
            var result = await mediator.Send(new UpdateRasporedCommand(id, request.InstruktorId,request.PolaznikId,request.DatumVreme));
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }

        [HttpDelete]
        public async Task<IActionResult>DeleteRaspored(int id)
        {
            var result = await mediator.Send(new RemoveRasporedCommand(id));
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.IsSuccess);
        }
    }
}
