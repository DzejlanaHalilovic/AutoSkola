using AutoSkola.Contracts.Models.Čas.Request;
using AutoSkola.Data.Models;
using AutoSkola.Infrastructure;
using AutoSkola.Mediator.Čas;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AutoSkola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CasController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly UserManager<User> userManager;
        private readonly IUnitOfWork unitOfWork;

        public CasController(IMediator mediator, UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            this.mediator = mediator;
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> getAll() => Ok(await mediator.Send(new GetAllCasQuery()));
        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            var result = await mediator.Send(new GetCasQuery(id));
            if (!result.IsSuccess)
                return NotFound(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult>CreateCas(CreateCasRequest request)
        {
            var result = await mediator.Send(new CreateCasCommand(request));
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteCas(int id)
        {
            var result = await mediator.Send(new RemoveCasCommand(id));
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.IsSuccess);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>UpdateCas(int id, UpdateČasRequest request)
        {
            var result = await mediator.Send(new UpdateCasCommand(id, request.RasporedId, request.AutomobilRegBroj));
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }

        [HttpGet("moji-casovi/ocene")]
        public async Task<ActionResult<List<Čas>>> GetOceneMojiCasovi()
        {
            // Dohvatite trenutno prijavljenog polaznika
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            if (currentUser == null)
            {
                // Korisnik nije prijavljen, vratite odgovarajući rezultat ili grešku
                return Unauthorized();
            }

            // Dohvatite rasporede u kojima je prijavljen polaznik
            var rasporedi = await unitOfWork.rasporedRepository.GetRasporediByPolaznikId(currentUser.Id);

            // Dohvatite časove za svaki raspored sa ocenama
            var casovi = new List<Čas>();
            foreach (var raspored in rasporedi)
            {
                var rasporedCasovi = await unitOfWork.časRepository.GetCasoviByRasporedId(raspored.Id);
                casovi.AddRange(rasporedCasovi);
            }

            // Vratite listu časova sa ocenama
            return Ok(casovi);
        }


    }
}
