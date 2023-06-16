using AutoMapper;
using AutoSkola.Contracts.Models;
using AutoSkola.Data.Models;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.Kategorija
{
    public class GetAllKategorijaQuery : IRequest<Result<IEnumerable<AutoSkola.Data.Models.Kategorija>>>
    {
    }

    public class GetKategorijaHandler : IRequestHandler<GetAllKategorijaQuery, Result<IEnumerable<AutoSkola.Data.Models.Kategorija>>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetKategorijaHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<Result<IEnumerable<Data.Models.Kategorija>>> Handle(GetAllKategorijaQuery request, CancellationToken cancellationToken)
        {
            var lista = await unitOfWork.kategorijaRepository.GetAll();
            var result = new Result<IEnumerable<AutoSkola.Data.Models.Kategorija>>
            {
                Data = lista
            };
            return result;
        }
    }
}
