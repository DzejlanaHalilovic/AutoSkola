using AutoSkola.Contracts.Models.PolaznikInstuktor;
using AutoSkola.Data;
using AutoSkola.Data.Models;
using AutoSkola.Infrastructure;
using AutoSkola.Mediator.PolaznikInstuktor;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoSkola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolaznikInstuktorController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IUnitOfWork unitOfWork;
        private readonly DataContext context;
        private readonly UserManager<User> userManager;

        public PolaznikInstuktorController(IMediator mediator, IUnitOfWork unitOfWork, DataContext context, UserManager<User> userManager)
        {
            this.mediator = mediator;
            this.unitOfWork = unitOfWork;
            this.context = context;
            this.userManager = userManager;
        }
        [HttpPost("korisnici")]
        public async Task<IActionResult> createRaspored(PolaznikInstuktorRequest request)
        {
            var result = await mediator.Send(new PolaznikInstuktorCommand(request));
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }
       

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await mediator.Send(new GetAllPolaznikInstuktorQuery()));

        [HttpGet("dostupni-instruktori/{polaznikId}")]
        public async Task<ActionResult<List<User>>> GetDostupniInstruktori(int polaznikId)
        {
            var polaznik = await userManager.FindByIdAsync(polaznikId.ToString());
            if (polaznik == null)
            {
                return BadRequest("Polaznik nije pronađen");
            }

            var listaSvihInstruktora = await userManager.GetUsersInRoleAsync("Instuktor");
            var instruktoriSaViseOdDvaPolaznika = context.polaznikinstuktor
                .GroupBy(pi => pi.InstruktorId)
                .Where(g => g.Count() >= 3)
                .Select(g => g.Key)
                .ToList();

            var polaznikovaKategorija = await context.userkategorija
                .Where(uk => uk.UserId == polaznik.Id)
                .Select(uk => uk.KategorijaId)
                .FirstOrDefaultAsync();

            var sviDostupniInstruktori = listaSvihInstruktora
                .Where(u => !instruktoriSaViseOdDvaPolaznika.Contains(u.Id))
                .Where(u => context.userkategorija
                    .Any(uk => uk.UserId == u.Id && uk.KategorijaId == polaznikovaKategorija))
                .Distinct()
                .ToList();

            return Ok(sviDostupniInstruktori);
        }







    }
}
