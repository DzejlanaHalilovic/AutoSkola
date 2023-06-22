using AutoSkola.Contracts.Models;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.Kvar
{
    public record RemoveKvarCommand(int id):IRequest<Result<bool>>
    {
    }
    public class RemoveKvarHandler : IRequestHandler<RemoveKvarCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;

        public RemoveKvarHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(RemoveKvarCommand request, CancellationToken cancellationToken)
        {
            var kvar = await unitOfWork.kvarRepository.getById(request.id);
            if(kvar == null)
            {
                return new Result<bool>
                {
                    Errors = new List<string> { "Nije pronadjeno" },
                    IsSuccess = false
                };
            }
            var result = await unitOfWork.kvarRepository.Delete(kvar);
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
