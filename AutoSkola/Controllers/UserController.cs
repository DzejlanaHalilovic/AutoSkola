using AutoMapper;
using AutoSkola.Contracts.Models.Identity.Request;
using AutoSkola.Contracts.Models.Identity.Response;
using AutoSkola.Data;
using AutoSkola.Data.Models;
using AutoSkola.Infrastructure;
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
        private readonly DataContext context;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public UserController(IMediator mediator, RoleManager<AppRole> role,DataContext context, UserManager<User> userManager,IMapper mapper,IUnitOfWork unitOfWork)
        {
            this.mediator = mediator;
            this.role = role;
            this.context = context;
            this.userManager = userManager;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
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
        [HttpGet("polaznicikategorija/{tipKategorije}")]
        public async Task<IActionResult> GetPolazniciByTipKategorije(string tipKategorije)
        {
            var polaznici = await userManager.GetUsersInRoleAsync("Polaznik");
            var polazniciKategorija = context.userkategorija
                .Where(uk => uk.Kategorija.Tip == tipKategorije && polaznici.Contains(uk.User))
                .Select(uk => uk.User)
                .ToList();

            return Ok(new { data = polazniciKategorija });
        }
        [HttpGet("instuktorikategorija/{tipKategorije}")]
        public async Task<IActionResult> GetInstuktoriByTipKategorije(string tipKategorije)
        {
            var polaznici = await userManager.GetUsersInRoleAsync("Instuktor");
            var polazniciKategorija = context.userkategorija
                .Where(uk => uk.Kategorija.Tip == tipKategorije && polaznici.Contains(uk.User))
                .Select(uk => uk.User)
                .ToList();

            return Ok(new { data = polazniciKategorija });
        }


        [HttpGet("kategorija/{tipKategorije}")]
        public async Task<IActionResult> GetPolazniciInstruktoriByKategorija(string tipKategorije)
        {
            var polaznici = await userManager.GetUsersInRoleAsync("Polaznik");
            var instruktori = await userManager.GetUsersInRoleAsync("Instruktor");

            var polazniciKategorija = context.userkategorija
                .Where(uk => uk.Kategorija.Tip == tipKategorije && polaznici.Contains(uk.User))
                .Select(uk => uk.User)
                .ToList();

            var instruktoriKategorija = context.userkategorija
                .Where(uk => uk.Kategorija.Tip == tipKategorije && instruktori.Contains(uk.User))
                .Select(uk => uk.User)
                .ToList();

            return Ok(new { polaznici = polazniciKategorija, instruktori = instruktoriKategorija });
        }
        [HttpGet("instruktori/dostupni/{categoryId}/{polaznikId}")]
        public async Task<ActionResult<List<UserResponse>>> GetAvailableInstructorsByCategoryAndPolaznik(int categoryId, string polaznikId)
        {
            var polaznik = await userManager.FindByIdAsync(polaznikId);

            if (polaznik == null)
            {
                return NotFound();
            }

            var instructors = await userManager.GetUsersInRoleAsync("Instructor");
            var availableInstructors = new List<UserResponse>();

            foreach (var instructor in instructors)
            {
                var instructorCategories = await userManager.GetRolesAsync(instructor);

                // Provera da li instruktor pripada zadatoj kategoriji
                if (instructorCategories.Contains(categoryId.ToString()))
                {
                    var instructorPolazniciCount = await unitOfWork.polaznikInstuktorRepository.GetPolazniciCountByInstruktorId(instructor.Id);

                    // Provera da li instruktor ima manje od 3 polaznika
                    if (instructorPolazniciCount < 3)
                    {
                        var instructorResponse = mapper.Map<UserResponse>(instructor);
                        availableInstructors.Add(instructorResponse);
                    }
                }
            }

            return availableInstructors;
        }








    }
}
