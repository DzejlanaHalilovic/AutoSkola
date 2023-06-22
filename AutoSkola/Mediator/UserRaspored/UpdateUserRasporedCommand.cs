using AutoSkola.Contracts.Models;
using AutoSkola.Contracts.Models.UserRaspored;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.UserRaspored
{
    public record UpdateUserRasporedCommand(int id,UserRasporedRequest request) : IRequest<Result<AutoSkola.Data.Models.UserRaspored>>
    {
    }

    public class UserRasporedHandler : IRequestHandler<UpdateUserRasporedCommand, Result<AutoSkola.Data.Models.UserRaspored>>
    {
        private readonly IUnitOfWork unitOfWork;

        public UserRasporedHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async  Task<Result<Data.Models.UserRaspored>> Handle(UpdateUserRasporedCommand request, CancellationToken cancellationToken)
        {
            if (request.request.DatumOdsustava == null)
                return new Result<AutoSkola.Data.Models.UserRaspored>
                {
                    Errors = new List<string> { "Datum je obavezno polje" },
                    IsSuccess = false
                };
            var odsustvo = await unitOfWork.userRasporedRepository.getById(request.id);
            if (odsustvo == null)
                return new Result<Data.Models.UserRaspored>
                {
                    Errors = new List<string> { $"Raspored nije nadjen za dati {request.id}" },
                    IsSuccess = false
                };
            odsustvo.Razlog = request.request.Razlog;
            odsustvo.DatumOdsustava = request.request.DatumOdsustava;
            var result = await unitOfWork.CompleteAsync();
            if (!result)
                return new Result<Data.Models.UserRaspored>
                {
                    Errors = new List<string> { "Erro in saving data" },
                    IsSuccess = false
                };
            return new Result<Data.Models.UserRaspored>
            {
                Data = odsustvo
            };

        }
    }
}
