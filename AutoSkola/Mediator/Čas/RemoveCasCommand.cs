using AutoSkola.Contracts.Models;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.Čas
{
    public record RemoveCasCommand(int id):IRequest<Result<bool>>
    {
    }

    public class RemoveCasHandler : IRequestHandler<RemoveCasCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;

        public RemoveCasHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(RemoveCasCommand request, CancellationToken cancellationToken)
        {
            var cas = await unitOfWork.časRepository.getById(request.id);
            if(cas == null)
            {
                return new Result<bool>
                {
                    Errors = new List<string> { "Cas nije pronadjen" },
                    IsSuccess = false
                };
            }
            var result = await unitOfWork.časRepository.Delete(cas);
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
