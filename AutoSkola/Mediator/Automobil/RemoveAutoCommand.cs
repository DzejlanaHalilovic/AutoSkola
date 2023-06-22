using AutoSkola.Contracts.Models;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.Automobil
{
    public record RemoveAutoCommand(int id):IRequest<Result<bool>>
    {
    }

    public class RemoveAutoHandler : IRequestHandler<RemoveAutoCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;

        public RemoveAutoHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(RemoveAutoCommand request, CancellationToken cancellationToken)
        {
            var auto = await unitOfWork.automobilRepository.getById(request.id);
            if(auto == null)
            {
                return new Result<bool>
                {
                    Errors = new List<string> { "Auto nije pronadjeno" },
                    IsSuccess = false
                };
            }
            var result = await unitOfWork.automobilRepository.Delete(auto);
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
