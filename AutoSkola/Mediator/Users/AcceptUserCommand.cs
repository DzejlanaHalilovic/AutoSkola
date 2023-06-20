using AutoSkola.Contracts.Models;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.Users
{
    public record AcceptUserCommand(int id): IRequest<Result<bool>>
    {
    }

    public class AcceptUserHandler : IRequestHandler<AcceptUserCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;

        public AcceptUserHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(AcceptUserCommand request, CancellationToken cancellationToken)
        {
            var result = await unitOfWork.userRepository.acceptUser(request.id);
            if (!result)
                return new Result<bool>
                {
                    Errors = new List<string> { $"The user with id {request.id} does not exist" },
                    IsSuccess = false
                };
            return new Result<bool>
            {
                IsSuccess = true
            };
                
        }
    }
}
