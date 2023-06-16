using AutoMapper;
using AutoSkola.Contracts.Models;
using AutoSkola.Contracts.Models.Raspored.Request;
using AutoSkola.Data.Models;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.Raspored
{
    public record CreateRasporedCommand(CreateRasporedRequest rasporedRequest): IRequest<Result<AutoSkola.Data.Models.Raspored>>;

    public class CreateRasporedHandler : IRequestHandler<CreateRasporedCommand, Result<AutoSkola.Data.Models.Raspored>>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public CreateRasporedHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public Task<Result<Data.Models.Raspored>> Handle(CreateRasporedCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
