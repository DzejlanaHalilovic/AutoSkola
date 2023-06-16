using AutoMapper;
using AutoSkola.Contracts.Models;
using AutoSkola.Contracts.Models.Kategorija;
using AutoSkola.Data.Models;
using AutoSkola.Infrastructure;
using MediatR;
using AutoSkola.Data.Models;
namespace AutoSkola.Mediator.Kategorija
{
    public record CreateKategorijaCommand (CreateKategorijaRequest kategorijaRequest) :IRequest<Result<AutoSkola.Data.Models.Kategorija>>;

    public class CreateKategorijaHandler : IRequestHandler<CreateKategorijaCommand, Result<AutoSkola.Data.Models.Kategorija>>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public CreateKategorijaHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<Data.Models.Kategorija>> Handle(CreateKategorijaCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.kategorijaRequest.Tip))
                return new Result<AutoSkola.Data.Models.Kategorija>
                {
                    Errors = new List<string> { "Tip is required" },
                    IsSuccess = false
                };

            var newKategorija = new AutoSkola.Data.Models.Kategorija
            {
                Tip = request.kategorijaRequest.Tip
            };
            await unitOfWork.kategorijaRepository.Add(newKategorija);
            var result = await unitOfWork.CompleteAsync();
            if (!result)
                return new Result<Data.Models.Kategorija>
                {
                    Errors = new List<string> { "Something is not correct with requets data" },
                    IsSuccess = false
                };
            return new Result<Data.Models.Kategorija> { Data = newKategorija };


        }
    }

}
