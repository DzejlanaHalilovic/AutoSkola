using AutoSkola.Contracts.Models.Kategorija;
using AutoSkola.Mediator.Kategorija;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoSkola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KategorijaController : ControllerBase
    {
        private readonly IMediator mediator;

        public KategorijaController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult>CreateKategorija(CreateKategorijaRequest request)
        {
            var result = await mediator.Send(new CreateKategorijaCommand(request));
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> getAll() => Ok(await mediator.Send(new GetAllKategorijaQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            var result = await mediator.Send(new GetKategorijaQuery(id));
            if (!result.IsSuccess)
                return NotFound(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }

        [HttpDelete]
        public async Task<IActionResult>DeleteKategorija(int id)
        {
            var result = await mediator.Send(new RemoveKategorijaCommand(id));
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.IsSuccess);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult>UpdateKategorija(int id, UpdateKategorijaRequest request)
        {
            var result = await mediator.Send(new UpdateKategorijaCommand(id, request.Tip));
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }

    }
}
