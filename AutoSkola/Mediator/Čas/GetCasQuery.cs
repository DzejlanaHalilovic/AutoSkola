using AutoMapper;
using AutoSkola.Contracts.Models;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.Čas
{
    public record GetCasQuery(int id): IRequest<Result<AutoSkola.Data.Models.Čas>>
    {
        
    }

    public class GetOneCasHandler : IRequestHandler<GetCasQuery, Result<AutoSkola.Data.Models.Čas>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetOneCasHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<Result<Data.Models.Čas>> Handle(GetCasQuery request, CancellationToken cancellationToken)
        {
            var cas = await unitOfWork.časRepository.getById(request.id);
            if(cas == null)
            {
                return new Result<Data.Models.Čas>
                {
                    Errors = new List<string> { $"cas pod tim id {request.id} nije pronadjen" },
                    IsSuccess = false
                };
            }
            return new Result<Data.Models.Čas>
            {
                Data = cas
            };
        }
    }

}
