using AutoSkola.Contracts.Models;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.Raspored
{
    public record RemoveRasporedCommand(int id): IRequest<Result<bool>>
    {

    }
    public class RemoveRasporedHandler : IRequestHandler<RemoveRasporedCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;

        public RemoveRasporedHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(RemoveRasporedCommand request, CancellationToken cancellationToken)
        {
            var raspored = await unitOfWork.rasporedRepository.getById(request.id);
            if(raspored == null)
            {
                return new Result<bool>
                {
                    Errors = new List<string> { "Raspored nije pronadjen" },
                    IsSuccess = false
                };
            }

            var result = await unitOfWork.rasporedRepository.Delete(raspored);
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
