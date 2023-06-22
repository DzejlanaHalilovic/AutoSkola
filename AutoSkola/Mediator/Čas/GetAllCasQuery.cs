using AutoMapper;
using AutoSkola.Contracts.Models;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.Čas
{
    public class GetAllCasQuery : IRequest<Result<IEnumerable<AutoSkola.Data.Models.Čas>>>
    {
    }

    public class GetCasHandler : IRequestHandler<GetAllCasQuery, Result<IEnumerable<AutoSkola.Data.Models.Čas>>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetCasHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<Result<IEnumerable<Data.Models.Čas>>> Handle(GetAllCasQuery request, CancellationToken cancellationToken)
        {
            var lista = await unitOfWork.časRepository.GetAll();
            var result = new Result<IEnumerable<AutoSkola.Data.Models.Čas>>
            {
                Data = lista
            };
            return result;
        }
    }
}
