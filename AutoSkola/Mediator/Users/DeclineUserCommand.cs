using AutoSkola.Contracts.Models;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.Users
{
    public record DeclineUserCommand(int id) : IRequest<Result<bool>>
    {
    }

    public class DeclineUserHandler : IRequestHandler<DeclineUserCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeclineUserHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(DeclineUserCommand request, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.userRepository.declineUser(request.id);

            if (!user)
                return new Result<bool>
                {
                    Errors = new List<string> { $"User with {request.id} does not exist" },
                    IsSuccess = false
                };
            return new Result<bool>
            {
                IsSuccess = true
            };

        }
    }
}
