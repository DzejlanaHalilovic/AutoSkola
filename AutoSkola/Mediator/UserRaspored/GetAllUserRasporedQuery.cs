using AutoMapper;
using AutoSkola.Contracts.Models;
using AutoSkola.Contracts.Models.Raspored.Response;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.UserRaspored
{
    public class GetAllUserRasporedQuery : IRequest<Result<IEnumerable<AutoSkola.Data.Models.UserRaspored>>>
    {
    }

    public class GetAllUserRasporedHandler : IRequestHandler<GetAllUserRasporedQuery, Result<IEnumerable<AutoSkola.Data.Models.UserRaspored>>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllUserRasporedHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<Result<IEnumerable<AutoSkola.Data.Models.UserRaspored>>> Handle(GetAllUserRasporedQuery request, CancellationToken cancellationToken)
        {
            var lista = await unitOfWork.userRasporedRepository.GetAll();
            var mapper2 = mapper.Map<List<AutoSkola.Data.Models.UserRaspored>>(lista);
            return new Result<IEnumerable<AutoSkola.Data.Models.UserRaspored>>
            {
                Data = mapper2
            };
        }
    }
}
