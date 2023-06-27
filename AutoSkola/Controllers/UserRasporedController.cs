using AutoSkola.Contracts.Models.UserRaspored;
using AutoSkola.Infrastructure;
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
        private readonly IUnitOfWork unitOfWork;

        public UserRasporedController(IMediator mediator,IUnitOfWork unitOfWork)
        {
            this.mediator = mediator;
            this.unitOfWork = unitOfWork;
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
        [HttpGet("odsustva/{userId}")]
        public async Task<IActionResult> GetOdsustvaByUserId(int userId)
        {
            var odsustva = await unitOfWork.userRasporedRepository.GetAll();
            var filteredOdsustva = odsustva.Where(o => o.UserId == userId).ToList();
            return Ok(filteredOdsustva);
        }
        [HttpDelete("odsustva/{userId}")]
        public async Task<IActionResult> DeleteOdsustvaByUserId(int userId)
        {
            try
            {
                var odsustva = await unitOfWork.userRasporedRepository.GetAll();
                var filteredOdsustva = odsustva.Where(o => o.UserId == userId).ToList();

                foreach (var odsustvo in filteredOdsustva)
                {
                    unitOfWork.userRasporedRepository.Delete(odsustvo);
                }

                await unitOfWork.CompleteAsync();

                return Ok("Odsustvo je uspešno obrisano.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Došlo je do greške prilikom brisanja odsustva.");
            }
        }



    }
}
