using AutoMapper;
using AutoSkola.Contracts.Models;
using AutoSkola.Contracts.Models.Identity.Response;
using AutoSkola.Data.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AutoSkola.Mediator.Users
{
    public record GetInstuktorListQuery : IRequest<Result<IEnumerable<UserResponse>>>
    {
    }

    public class GetInstuktorListHandler : IRequestHandler<GetInstuktorListQuery, Result<IEnumerable<UserResponse>>>
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public GetInstuktorListHandler(UserManager<User> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }
        public async Task<Result<IEnumerable<UserResponse>>> Handle(GetInstuktorListQuery request, CancellationToken cancellationToken)
        {
            var instuktorList = new List<UserResponse>();

            var list = await userManager.GetUsersInRoleAsync("Instuktor");
            foreach (var user in list)
            {
                if (!user.EmailConfirmed)
                {
                    var userWithKategorija = await userManager.Users.Include(u => u.userkategorija)
                                                                  .FirstOrDefaultAsync(u => u.Id == user.Id);
                    var mappedUser = mapper.Map<UserResponse>(userWithKategorija);
                    instuktorList.Add(mappedUser);
                }
            }

            if (instuktorList.Count == 0)
            {
                return new Result<IEnumerable<UserResponse>>
                {
                    Errors = new List<string> { "There are no instructors" },
                    IsSuccess = false
                };
            }

            return new Result<IEnumerable<UserResponse>>
            {
                Data = instuktorList
            };
        }

    }
}
