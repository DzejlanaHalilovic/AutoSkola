using AutoSkola.Contracts.Models;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.Automobil
{
    public class GetAutaKategorijaQuery :  IRequest<Result<IEnumerable<Data.Models.Automobil>>>
    {
    }


    public class GetAutoKategorijaHandler : IRequestHandler<GetAllAutoQuery, Result<IEnumerable<Data.Models.Automobil>>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetAutoKategorijaHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Task<Result<IEnumerable<Data.Models.Automobil>>> Handle(GetAllAutoQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
