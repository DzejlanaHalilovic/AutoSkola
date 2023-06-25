using AutoSkola.Contracts.Models.PolaznikInstuktor;
using AutoSkola.Data;
using AutoSkola.Infrastructure;
using AutoSkola.Mediator.PolaznikInstuktor;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoSkola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolaznikInstuktorController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IUnitOfWork unitOfWork;
        private readonly DataContext context;

        public PolaznikInstuktorController(IMediator mediator, IUnitOfWork unitOfWork, DataContext context)
        {
            this.mediator = mediator;
            this.unitOfWork = unitOfWork;
            this.context = context;
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
    }
}
