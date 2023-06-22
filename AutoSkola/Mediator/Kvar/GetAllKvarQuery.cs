using AutoSkola.Contracts.Models;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.Kvar
{
    public class GetAllKvarQuery :IRequest<Result<IEnumerable<Data.Models.Kvar>>>
    {
    }
    public class GetAllKvar : IRequestHandler<GetAllKvarQuery, Result<IEnumerable<Data.Models.Kvar>>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetAllKvar(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<IEnumerable<Data.Models.Kvar>>> Handle(GetAllKvarQuery request, CancellationToken cancellationToken)
        {
            var lista = await unitOfWork.kvarRepository.GetAll();
            return new Result<IEnumerable<Data.Models.Kvar>> { Data = lista };
        }
    }
}
