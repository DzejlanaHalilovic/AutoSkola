using AutoMapper;
using AutoSkola.Contracts.Models;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.Kategorija
{
    public record GetKategorijaQuery(int id):IRequest<Result<AutoSkola.Data.Models.Kategorija>>;

    public class GetOneKategorijaHandler : IRequestHandler<GetKategorijaQuery, Result<AutoSkola.Data.Models.Kategorija>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetOneKategorijaHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<Result<Data.Models.Kategorija>> Handle(GetKategorijaQuery request, CancellationToken cancellationToken)
        {
            var kategorija = await unitOfWork.kategorijaRepository.getById(request.id);
            if(kategorija == null)
            {
                return new Result<Data.Models.Kategorija>
                {
                    Errors = new List<string> { $"Kategorija with{request.id} not  found in database" },
                    IsSuccess = false
                };
               
            } return new Result<Data.Models.Kategorija> { Data = kategorija };
        }
    }
}
