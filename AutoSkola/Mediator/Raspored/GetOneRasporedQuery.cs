using AutoSkola.Contracts.Models;
using MediatR;
using AutoSkola.Data.Models;
using AutoMapper;
using AutoSkola.Infrastructure;

namespace AutoSkola.Mediator.Raspored
{
    public record GetOneRasporedQuery(int id): IRequest<Result<AutoSkola.Data.Models.Raspored>>
    {
    }

    public class GetOneRasporedHandler : IRequestHandler<GetOneRasporedQuery, Result<AutoSkola.Data.Models.Raspored>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetOneRasporedHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<Result<Data.Models.Raspored>> Handle(GetOneRasporedQuery request, CancellationToken cancellationToken)
        {
            var raspored = await unitOfWork.rasporedRepository.getById(request.id);
            if(raspored == null)
            {
                return new Result<Data.Models.Raspored>
                {
                    Errors = new List<string> { $"Raspored pod tim id {request.id} nije pronadjen" },
                    IsSuccess = false
                };
            }
            return new Result<Data.Models.Raspored>
            {
                Data = raspored
            };
        }
    }
}
