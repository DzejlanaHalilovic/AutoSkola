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
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly RoleManager<AppRole> role;

        public UserController(IMediator mediator, RoleManager<AppRole> role)
        {
            this.mediator = mediator;
            this.role = role;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers() => Ok(await mediator.Send(new GetAllUsersQuery()));
        
        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            var result = await mediator.Send(new GetUserQuery(id));
            if (!result.IsSuccess)
                return NotFound(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteUser(int id)
        {
            var result = await mediator.Send(new DeleteUserCommand(id));
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.IsSuccess);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>UpdateUser(int id, UpdateUserRequest request)
        {
            var result = await mediator.Send(new UpdateUserCommand(id, request));
            if (!result.IsSuccess)
                return NotFound(result.Errors.FirstOrDefault());
            return Ok(result.Data);

        }

        [HttpGet("/role")]
        public async Task<IActionResult> getRoles()
        {
            User user = new User();
            var lista = await role.Roles.ToListAsync();
            return Ok(lista);
        }

        [HttpGet("/polaznik-list")]
        public async Task<IActionResult> getPolaznik()
        {
            var result = await mediator.Send(new GetPolaznikListQuery());
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }
        [HttpGet("/instuktor-list")]
        public async Task<IActionResult> getInstuktor()
        {
            var result = await mediator.Send(new GetInstuktorListQuery());
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }

        [HttpPut("accept/{id}")]

        public async Task<IActionResult>acceptPolaznik(int id)
        {
            var result = await mediator.Send(new AcceptUserCommand(id));
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.IsSuccess);
        }

        [HttpDelete("decline/{id}")]
        public async Task<IActionResult>declinePolaznik(int id)
        {
            var result = await mediator.Send(new DeclineUserCommand(id));
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.IsSuccess);
        }


    }
}
