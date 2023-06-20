using AutoMapper;
using AutoSkola.Contracts.Models;
using AutoSkola.Data.Models;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.Raspored
{
    public class GetAllRasporedQuery : IRequest<Result<IEnumerable<AutoSkola.Data.Models.Raspored>>>
    {

    }

    public class GetRasporedHandler : IRequestHandler<GetAllRasporedQuery, Result<IEnumerable<AutoSkola.Data.Models.Raspored>>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetRasporedHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<Result<IEnumerable<Data.Models.Raspored>>> Handle(GetAllRasporedQuery request, CancellationToken cancellationToken)
        {
            var lista = await unitOfWork.rasporedRepository.GetAll();
            var result = new Result<IEnumerable<AutoSkola.Data.Models.Raspored>>
            {
                Data = lista
            };
            return result;
        }
    }
}
