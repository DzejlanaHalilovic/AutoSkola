using AutoMapper;
using AutoSkola.Contracts.Models;
using AutoSkola.Contracts.Models.Identity.Response;
using AutoSkola.Data.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Globalization;

namespace AutoSkola.Mediator.Users
{
    public record GetUserQuery(int id): IRequest<Result<UserResponse>>
    {
    }

    public class GetUserHandler : IRequestHandler<GetUserQuery, Result<UserResponse>>
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        public GetUserHandler(UserManager<User> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;

        }

        public async Task<Result<UserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
           var user = await userManager.FindByIdAsync(request.id.ToString());
            if(user == null)
            {
                return new Result<UserResponse>
                {
                    Errors = new List<string> { $"User with {request.id} is not found" },
                    IsSuccess = false
                };
                

            }

           
            var mappedUser = mapper.Map<UserResponse>(user);
          
            return new Result<UserResponse>
                {
                    Data = mappedUser
                };
        }
    }
}
