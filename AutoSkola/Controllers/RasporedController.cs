using AutoSkola.Contracts.Models.PolaznikInstuktor;
using AutoSkola.Contracts.Models.Raspored.Request;
using AutoSkola.Data;
using AutoSkola.Infrastructure;
using AutoSkola.Mediator.PolaznikInstuktor;
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

        [HttpPost("korisnici")]
        public async Task<IActionResult> createRaspored(PolaznikInstuktorRequest request)
        {
            var result = await mediator.Send(new PolaznikInstuktorCommand(request));
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

        [HttpGet("polaznik/{id}")]
        public async Task<IActionResult>getPolaznikRaspored(int id)
        {
            var result = await unitOfWork.rasporedRepository.getrasporedzapolaznika(id);
            if (result == null)
                return null;
            return Ok(result);
        }

        [HttpGet("instuktor/{id}")]
        public async Task<IActionResult> getInstuktorRaspored(int id)
        {
            var result = await unitOfWork.rasporedRepository.getrasporedzaintukora(id);
            if (result == null)
                return null;
            return Ok(result);
        }
        [HttpGet("instuktorfilter/{id}")]
        public async Task<IActionResult> getInstuktorRasporedfilter(int id)
        {
            var result = await unitOfWork.rasporedRepository.GetTop10RasporedaZaInstruktora(id);
            return Ok(result);
        }
        [HttpGet("filterpodatumu/{datum}/instukor/{id}")]
        public async Task<IActionResult> getInstuktorRaspored(int id, DateTime? datum)
        {
            var result = await unitOfWork.rasporedRepository.GetFilteredRasporedaZaInstruktora(id, datum);
            return Ok(result);
        }
        [HttpPost("nijepostovanraspored")]
        public async Task<IActionResult> NijePostovanRaspored(CreateRasporedRequest request)
        {
            var result = await mediator.Send(new CreateOdsustvoRasporedCommand(request));
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }


    }
}
