using AutoSkola.Contracts.Models;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.UserRaspored
{
    public record RemoveUserRasporedCommand(int id): IRequest<Result<bool>>
    {
    }

    public class RemoveUserRasporedHandler : IRequestHandler<RemoveUserRasporedCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;

        public RemoveUserRasporedHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(RemoveUserRasporedCommand request, CancellationToken cancellationToken)
        {
            var odsustvo = await unitOfWork.userRasporedRepository.getById(request.id);
            if(odsustvo == null)
            {
                return new Result<bool>
                {
                    Errors = new List<string> { "Odsustvo nije pronadjeno" },
                    IsSuccess = false
                };
            }
            var result = await unitOfWork.userRasporedRepository.Delete(odsustvo);
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
