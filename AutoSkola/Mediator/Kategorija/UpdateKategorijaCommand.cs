using AutoMapper;
using AutoSkola.Contracts.Models;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.Kategorija
{
    public record UpdateKategorijaCommand(int id,string tip):IRequest<Result<AutoSkola.Data.Models.Kategorija>>;

    public class UpdateKategorijaHandler : IRequestHandler<UpdateKategorijaCommand, Result<AutoSkola.Data.Models.Kategorija>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UpdateKategorijaHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async  Task<Result<Data.Models.Kategorija>> Handle(UpdateKategorijaCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.tip))
                return new Result<AutoSkola.Data.Models.Kategorija>
                {
                    Errors = new List<string> { "Tip  is required" },
                    IsSuccess = false
                };
            var kategorija = await unitOfWork.kategorijaRepository.getById(request.id);
            if (kategorija == null)
                return new Result<Data.Models.Kategorija>
                {
                    Errors = new List<string> { "Kategorija is not found" },
                    IsSuccess = false
                };
            kategorija.Tip = request.tip;
            var result = await unitOfWork.CompleteAsync();
            if (!result)
                return new Result<Data.Models.Kategorija>
                {
                    Errors = new List<string> { "Error in saving datta" },
                    IsSuccess = false
                };

            return new Result<Data.Models.Kategorija> { Data = kategorija };


        }
    }
}
