using AutoSkola.Contracts.Models;
using AutoSkola.Contracts.Models.Automobil.Request;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.Automobil
{
    public record UpdateAutoCommand(int id, CreateAutomobilRequest request):IRequest<Result<Data.Models.Automobil>>
    {
    }

    public class UpdateAutoHandler : IRequestHandler<UpdateAutoCommand, Result<Data.Models.Automobil>>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateAutoHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<Data.Models.Automobil>> Handle(UpdateAutoCommand request, CancellationToken cancellationToken)
        {
            if (request.request.Model == null)
                return new Result<Data.Models.Automobil>
                {
                    Errors = new List<string> { "Obavezno polje" },
                    IsSuccess = false
                };
            var auto = await unitOfWork.automobilRepository.getById(request.id);
            if (auto == null)
                return new Result<Data.Models.Automobil>
                {
                    Errors = new List<string> { "auto nije pronadjeno sa tim id" },
                    IsSuccess = false
                };
            auto.Model = request.request.Model;
            var result = await unitOfWork.CompleteAsync();
            if (!result)
                return new Result<Data.Models.Automobil>
                {
                    Errors = new List<string> { "error in saving data" },
                    IsSuccess = false
                };
            return new Result<Data.Models.Automobil>
            {
                Data = auto
            };
        }
    }
}
