using AutoSkola.Contracts.Models;
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

        public GetAllUserRasporedHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<IEnumerable<Data.Models.UserRaspored>>> Handle(GetAllUserRasporedQuery request, CancellationToken cancellationToken)
        {
            var lista = await unitOfWork.userRasporedRepository.GetAll();
            return new Result<IEnumerable<Data.Models.UserRaspored>>
            {
                Data = lista
            };
        }
    }
}
