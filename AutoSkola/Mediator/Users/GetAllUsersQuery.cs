using AutoMapper;
using AutoSkola.Contracts.Models;
using AutoSkola.Contracts.Models.Identity.Response;
using AutoSkola.Data.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AutoSkola.Mediator.Users
{
    public record GetAllUsersQuery : IRequest<Result<IEnumerable<UserResponse>>> { }

    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, Result<IEnumerable<UserResponse>>>
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        public GetAllUsersHandler(UserManager<User> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }
        public async Task<Result<IEnumerable<UserResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var lista = await userManager.Users.ToListAsync();
            var mappedList = mapper.Map<List<UserResponse>>(lista);
            return new Result<IEnumerable<UserResponse>>
            {
                Data = mappedList
            };
        }
    }
}
