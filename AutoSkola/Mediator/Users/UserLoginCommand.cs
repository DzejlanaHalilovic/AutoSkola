using AutoSkola.Contracts.Models;
using AutoSkola.Contracts.Models.Identity.Request;
using AutoSkola.Contracts.Models.Identity.Response;
using AutoSkola.Exceptions;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.Users
{
    public record UserLoginCommand(LoginRequest Request) : IRequest<Result<LoginResponse>>
    {
    }

    public class UserLoginHandler : IRequestHandler<UserLoginCommand, Result<LoginResponse>>
    {
        private readonly IUnitOfWork unitOfWork;

        public UserLoginHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<LoginResponse>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Request.Email) || string.IsNullOrEmpty(request.Request.Password))
                throw new LoginCustomException("Both inputs are required");
            var result = await unitOfWork.userRepository.login(request.Request);
            if(result.error != "")
            {
                return new Result<LoginResponse>
                {
                    Errors = new List<string> { result.error },
                    IsSuccess = false
                };
            }
            return new Result<LoginResponse>
            {
                Data = result
            };
        }
    }
}


