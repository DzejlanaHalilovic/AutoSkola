using AutoMapper;
using AutoSkola.Contracts.Models;
using AutoSkola.Contracts.Models.Kategorija;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.Kategorija
{
    public record GetKategorijaQuery(int id):IRequest<Result<CreateKategorijaResponse>>;

    public class GetOneKategorijaHandler : IRequestHandler<GetKategorijaQuery, Result<CreateKategorijaResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetOneKategorijaHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<Result<CreateKategorijaResponse>> Handle(GetKategorijaQuery request, CancellationToken cancellationToken)
        {
            var kategorija = await unitOfWork.kategorijaRepository.getById(request.id);
            var response = mapper.Map<CreateKategorijaResponse>(kategorija);
            if (kategorija == null)
            {
                return new Result<CreateKategorijaResponse>
                {
                    Errors = new List<string> { $"Kategorija with{request.id} not  found in database" },
                    IsSuccess = false
                };
               
            }
            return new Result<CreateKategorijaResponse> { 
                Data = response };
        }
    }
}
