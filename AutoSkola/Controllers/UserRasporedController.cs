using AutoSkola.Contracts.Models.UserRaspored;
using AutoSkola.Mediator.UserRaspored;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoSkola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRasporedController : ControllerBase
    {
        private readonly IMediator mediator;

        public UserRasporedController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> getAll() => Ok(await mediator.Send(new GetAllUserRasporedQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            var result = await mediator.Send(new GetUserRasporedQuery(id));
            if (!result.IsSuccess)
                return NotFound(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }


        [HttpPost]
        public async Task<IActionResult>CreateOdsustvo(UserRasporedRequest request)
        {
            var result = await mediator.Send(new CreateUserRasporedCommand(request));
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>deleteRaspored(int id)
        {
            var result = await mediator.Send(new RemoveUserRasporedCommand(id));
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.IsSuccess);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOdsustvo(int id, UserRasporedRequest request)
        {
            var result = await mediator.Send(new UpdateUserRasporedCommand(id, request));
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }
    }
}
