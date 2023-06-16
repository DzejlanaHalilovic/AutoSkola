using AutoSkola.Contracts.Models;
using AutoSkola.Contracts.Models.Identity.Request;
using AutoSkola.Contracts.Models.Identity.Response;
using AutoSkola.Infrastructure;
using MediatR;
using System.Runtime.InteropServices;

namespace AutoSkola.Mediator.Users
{
    public record UserRegisterCommand(RegisterRequest request) : IRequest<Result<UserResponse>>
    {
    }

    public class UserRegisterHandler : IRequestHandler<UserRegisterCommand, Result<UserResponse>>
    {
        private readonly IUnitOfWork unitOfWork;

        public UserRegisterHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<UserResponse>> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            var result = await unitOfWork.userRepository.register(request.request);
            if(result == null)
            {
                return new Result<UserResponse>
                {
                    Errors = new List<string> { "Something went wrong, try again" },
                    IsSuccess = false
                };
            }
            return new Result<UserResponse>
            {
                Data = result
            };
        }
    }
}
