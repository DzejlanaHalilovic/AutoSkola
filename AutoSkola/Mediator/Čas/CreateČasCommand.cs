using AutoMapper;
using AutoSkola.Contracts.Models;
using AutoSkola.Contracts.Models.Čas.Request;
using AutoSkola.Data.Models;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.Čas
{
    public record CreateCasCommand(CreateCasRequest casrequest) :IRequest<Result<AutoSkola.Data.Models.Čas>>
    {

    }

    public class CreateCasHandler : IRequestHandler<CreateCasCommand, Result<AutoSkola.Data.Models.Čas>>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public CreateCasHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<AutoSkola.Data.Models.Čas>> Handle(CreateCasCommand request, CancellationToken cancellationToken)
        {
            if (request.casrequest.RasporedId == null)
                return new Result<AutoSkola.Data.Models.Čas>
                {
                   Errors = new List<string> { "obavezno polje" },
                   IsSuccess = false
               };
            var newcas = new AutoSkola.Data.Models.Čas
            {
                RasporedId = request.casrequest.RasporedId,
                //idauta = request.casrequest.AutomobilRegBroj,
                Ocena = request.casrequest.Ocena
            };
            await unitOfWork.časRepository.Add(newcas);
            var result = await unitOfWork.CompleteAsync();
            if (!result)
                return new Result<Data.Models.Čas>
                {
                    Errors = new List<string> { "pokusaj ponovo" },
                    IsSuccess = false
                };
            return new Result<Data.Models.Čas>
            {
                Data = newcas
            };
        }
    }
}
