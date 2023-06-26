using AutoMapper;
using AutoSkola.Contracts.Models;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.Čas
{
    public record UpdateCasCommand(int id, int rasporedId, int automobilId) : IRequest<Result<AutoSkola.Data.Models.Čas>>
    {
    }

    public class UpdateCasHandler : IRequestHandler<UpdateCasCommand, Result<AutoSkola.Data.Models.Čas>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UpdateCasHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<Result<Data.Models.Čas>> Handle(UpdateCasCommand request, CancellationToken cancellationToken)
        {
            var cas = await unitOfWork.časRepository.getById(request.id);
            if (cas == null)
                return new Result<Data.Models.Čas>
                {
                    Errors = new List<string> { "Obavezno polje" },
                    IsSuccess = false
                };
            cas.RasporedId = request.rasporedId;
            cas.Id = request.automobilId;
            var result = await unitOfWork.CompleteAsync();
            if (!result)
                return new Result<Data.Models.Čas>
                {
                    Errors = new List<string> { "error in saving data" },
                    IsSuccess = false
                };
            return new Result<Data.Models.Čas>
            {
                Data = cas
            };
        }
    }
}
