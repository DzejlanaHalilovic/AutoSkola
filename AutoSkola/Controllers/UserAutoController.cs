using AutoSkola.Contracts.Models.Automobil.Request;
using AutoSkola.Mediator.Automobil;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoSkola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAutoController : ControllerBase
    {
        private readonly IMediator mediator;

        public UserAutoController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult>createUserAuto(CreateUserAutoRequest request)
        {
            var result = await mediator.Send(new UserAutomobilCommand(request));
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }
    }
}
