using AutoMapper;
using AutoSkola.Contracts.Models;
using AutoSkola.Contracts.Models.Identity.Response;
using AutoSkola.Data.Models;
using AutoSkola.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AutoSkola.Mediator.Users
{
    public record GetPolaznikListQuery : IRequest<Result<IEnumerable<UserResponse>>>
    {
    }

    public class GetPolaznikListHandler : IRequestHandler<GetPolaznikListQuery, Result<IEnumerable<UserResponse>>>
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public GetPolaznikListHandler(UserManager<User> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }
        public async Task<Result<IEnumerable<UserResponse>>> Handle(GetPolaznikListQuery request, CancellationToken cancellationToken)
        {
            var list = await userManager.GetUsersInRoleAsync("Polaznik");
            var polaznikList = new List<User>();
            foreach (var user in list)
            {
                if (!user.EmailConfirmed)
                    polaznikList.Add(user);
            }
            var mapped = mapper.Map<List<UserResponse>>(polaznikList);
            if (polaznikList == null)
                return new Result<IEnumerable<UserResponse>>
                {
                    Errors = new List<string> { "There are not polaznik" },
                    IsSuccess = false
                };
            return new Result<IEnumerable<UserResponse>>
            {
                Data = mapped
            };

        }
    }
}
