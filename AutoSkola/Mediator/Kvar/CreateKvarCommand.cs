using AutoMapper;
using AutoSkola.Contracts.Models;
using AutoSkola.Contracts.Models.Kvar.Request;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.Kvar
{
    public record CreateKvarCommand(CreateKvarRequest request):IRequest<Result<Data.Models.Kvar>>
    {
    }

    public class CreateKvarHandler : IRequestHandler<CreateKvarCommand, Result<Data.Models.Kvar>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateKvarHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async  Task<Result<Data.Models.Kvar>> Handle(CreateKvarCommand request, CancellationToken cancellationToken)
        {
            if (request.request.Opis == null)
                return new Result<Data.Models.Kvar>
                {
                    Errors = new List<string> { "Obavezno polje" },
                    IsSuccess = false
                };

            var kvar = new Data.Models.Kvar
            {
                Opis = request.request.Opis,
                DatumKvara = request.request.DatumKvara,
                AutomobilRegBroj = request.request.AutomobilRegBroj
            };
            await unitOfWork.kvarRepository.Add(kvar);
            var result = await unitOfWork.CompleteAsync();
            if (!result)
                return new Result<Data.Models.Kvar>
                {
                    Errors = new List<string> { "Error in adding  data" },
                    IsSuccess = false
                };
            return new Result<Data.Models.Kvar> { Data = kvar };
        }
    }
}
