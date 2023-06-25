using AutoSkola.Contracts.Models;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.PolaznikInstuktor
{
    public record PolaznikInstuktorRemoveCommand(int id):IRequest<Result<bool>>
    {
    }

    public class RemovePolaznikInszuktorHandler : IRequestHandler<PolaznikInstuktorRemoveCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;

        public RemovePolaznikInszuktorHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(PolaznikInstuktorRemoveCommand request, CancellationToken cancellationToken)
        {
            var userpolaznika = await unitOfWork.polaznikInstuktorRepository.getById(request.id);
            if(userpolaznika == null)
            {
                return new Result<bool>
                {
                    Errors = new List<string> { "nije pronadjeno" },
                    IsSuccess = false
                };
            }

            var result = await unitOfWork.polaznikInstuktorRepository.Delete((userpolaznika));
            if (!result)
                return new Result<bool>
                {
                    IsSuccess = false
                };
            return new Result<bool>
            {
                IsSuccess = true
            };
        }
    }
}
