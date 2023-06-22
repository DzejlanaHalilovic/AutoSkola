using AutoSkola.Contracts.Models;
using AutoSkola.Contracts.Models.Kvar.Request;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.Kvar
{
    public record UpdateKvarCommand(int id, CreateKvarRequest request):IRequest<Result<Data.Models.Kvar>>
    {
    }

    public class UpdateKvarHandler : IRequestHandler<UpdateKvarCommand, Result<Data.Models.Kvar>>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateKvarHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async  Task<Result<Data.Models.Kvar>> Handle(UpdateKvarCommand request, CancellationToken cancellationToken)
        {
            if (request.request.Opis == null)
                return new Result<Data.Models.Kvar>
                {
                    Errors = new List<string> { "Opis je obavezno polje" },
                    IsSuccess = false
                };
            var kvar = await unitOfWork.kvarRepository.getById(request.id);
            if (kvar == null)
                return new Result<Data.Models.Kvar>
                {
                    Errors = new List<string> { "Nije pronadje kvar" },
                    IsSuccess = false
                };
            kvar.Opis = request.request.Opis;
            kvar.DatumKvara = request.request.DatumKvara;
            var result = await unitOfWork.CompleteAsync();
            if (!result) return new Result<Data.Models.Kvar>
            {
                Errors = new List<string> { "Error in saving data" },
                IsSuccess = false
            };
            return new Result<Data.Models.Kvar>
            {
                Data = kvar
            };
        }
    }

}
