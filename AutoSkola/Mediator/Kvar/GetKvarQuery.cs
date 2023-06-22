using AutoSkola.Contracts.Models;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.Kvar
{
    public record GetKvarQuery(int id):IRequest<Result<Data.Models.Kvar>>
    {
    }

    public class GetKvarHandler : IRequestHandler<GetKvarQuery, Result<Data.Models.Kvar>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetKvarHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<Data.Models.Kvar>> Handle(GetKvarQuery request, CancellationToken cancellationToken)
        {
            var kvar = await unitOfWork.kvarRepository.getById(request.id);
            if(kvar == null)
            {
                return new Result<Data.Models.Kvar>
                {
                    Errors = new List<string> { "Nije pronadjen kvar" },
                    IsSuccess = false
                };
            }
            return new Result<Data.Models.Kvar>
            {
                Data = kvar
            };
        }
    }
}
