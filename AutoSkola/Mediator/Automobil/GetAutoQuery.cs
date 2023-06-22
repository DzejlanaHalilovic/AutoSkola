using AutoSkola.Contracts.Models;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.Automobil
{
    public record GetAutoQuery(int id): IRequest<Result<Data.Models.Automobil>>
    {
    }

    public class GetAutoHandler : IRequestHandler<GetAutoQuery, Result<Data.Models.Automobil>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetAutoHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<Data.Models.Automobil>> Handle(GetAutoQuery request, CancellationToken cancellationToken)
        {
            var auto = await unitOfWork.automobilRepository.getById(request.id);
            if(auto == null)
            {
                return new Result<Data.Models.Automobil>
                {
                    Errors = new List<string> { "auto sa registracijom nije nadjeno" },
                    IsSuccess = false
                };
            }
            return new Result<Data.Models.Automobil>
            {
                Data = auto
            };
        }
    }
}
