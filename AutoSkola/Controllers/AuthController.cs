using AutoSkola.Contracts.Models.Identity.Request;
using AutoSkola.Data.Models;
using AutoSkola.Mediator.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoSkola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IMediator mediator;

        public AuthController(UserManager<User> userManager, IMediator mediator)
        {
            this.userManager = userManager;
            this.mediator = mediator;
        }

        [HttpPost("check-token")]
        public async Task<IActionResult> checkToken([FromBody] string token)
        {
            var user = await userManager.Users.Where(x => x.SecurityStamp == token).FirstOrDefaultAsync();
            if (user == null)
                return BadRequest(new { error = "User with this token does not exist" });
            return Ok(true);

        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> register(RegisterRequest request)
        {
            var result = await mediator.Send(new UserRegisterCommand(request));
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }

        [HttpPost]
        [Route("login")]

        public async Task<IActionResult>login(LoginRequest request)
        {
            var result = await mediator.Send(new UserLoginCommand(request));
            if (!result.IsSuccess)
                return BadRequest(new { msg = result.Errors.FirstOrDefault() });
            return Ok(result.Data);
        }
    }
}
