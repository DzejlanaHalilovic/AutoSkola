using AutoMapper;
using AutoSkola.Contracts.Models;
using AutoSkola.Contracts.Models.Raspored.Response;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.UserRaspored
{
    public class GetAllUserRasporedQuery : IRequest<Result<IEnumerable<RasporedResponse>>>
    {
    }

    public class GetAllUserRasporedHandler : IRequestHandler<GetAllUserRasporedQuery, Result<IEnumerable<RasporedResponse>>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllUserRasporedHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<Result<IEnumerable<RasporedResponse>>> Handle(GetAllUserRasporedQuery request, CancellationToken cancellationToken)
        {
            var lista = await unitOfWork.userRasporedRepository.GetAll();
            var mapper2 = mapper.Map<List<RasporedResponse>>(lista);
            return new Result<IEnumerable<RasporedResponse>>
            {
                Data = mapper2
            };
        }
    }
}
