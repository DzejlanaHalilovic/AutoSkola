using AutoSkola.Contracts.Models;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.Automobil
{
    public class GetAllAutoQuery : IRequest<Result<IEnumerable<Data.Models.Automobil>>>
    {

    }

    public class GetAllHandler : IRequestHandler<GetAllAutoQuery, Result<IEnumerable<Data.Models.Automobil>>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetAllHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<IEnumerable<Data.Models.Automobil>>> Handle(GetAllAutoQuery request, CancellationToken cancellationToken)
        {
            var lista = await unitOfWork.automobilRepository.GetAll();
            return new Result<IEnumerable<Data.Models.Automobil>>
            {
                Data = lista
            };
        }
    }
}
