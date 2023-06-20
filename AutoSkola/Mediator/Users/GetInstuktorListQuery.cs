using AutoMapper;
using AutoSkola.Contracts.Models;
using AutoSkola.Contracts.Models.Identity.Response;
using AutoSkola.Data.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

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
            var list = await userManager.GetUsersInRoleAsync("Instuktor");
            var instuktorList = new List<User>();
            foreach(var user in list)
            {
                if (!user.EmailConfirmed)
                    instuktorList.Add(user);
            }
            var mapped = mapper.Map<List<UserResponse>>(instuktorList);
            if (instuktorList == null)
                return new Result<IEnumerable<UserResponse>>
                {
                    Errors = new List<string> { "There are not instuctors" },
                    IsSuccess = false
                };
            return new Result<IEnumerable<UserResponse>>
            {
                Data = mapped
            };
        }
    }
}
