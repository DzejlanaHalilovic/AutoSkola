using AutoMapper;
using AutoSkola.Contracts.Models;
using AutoSkola.Contracts.Models.Identity.Request;
using AutoSkola.Contracts.Models.Identity.Response;
using AutoSkola.Data.Models;
using AutoSkola.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AutoSkola.Mediator.Users
{
    public record UpdateUserCommand(int id, UpdateUserRequest request):IRequest<Result<UserResponse>>
    {
    }

    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Result<UserResponse>>
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public UpdateUserHandler(UserManager<User>userManager, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public  async Task<Result<UserResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.request.Ime))
                return new Result<UserResponse>
                {
                    Errors = new List<string> { "Name is required" },
                    IsSuccess = false
                };
            else if (string.IsNullOrEmpty(request.request.Prezime))
                return new Result<UserResponse>
                {
                    Errors = new List<string> { "Surname is required" },
                    IsSuccess = false
                };
            else if (string.IsNullOrEmpty(request.request.BrojTelefona))
                return new Result<UserResponse>
                {
                    Errors = new List<string> { "Number is required" },
                    IsSuccess = false
                };

            var user = await userManager.FindByIdAsync(request.id.ToString());
            if (user == null)
                return new Result<UserResponse>
                {
                    Errors = new List<string> { "User is not found" },
                    IsSuccess = false
                };
            mapper.Map<UpdateUserRequest, User>(request.request, user);
            await unitOfWork.CompleteAsync();
            var mappedUser = mapper.Map<UserResponse>(user);
            return new Result<UserResponse>
            {
                Data = mappedUser
            };

           
        }
    }
}
