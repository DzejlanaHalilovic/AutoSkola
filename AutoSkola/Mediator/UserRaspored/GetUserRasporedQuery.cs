using AutoSkola.Contracts.Models;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.UserRaspored
{
    public record GetUserRasporedQuery(int id): IRequest<Result<AutoSkola.Data.Models.UserRaspored>>
    {
    }

    public class GetUserRaspored : IRequestHandler<GetUserRasporedQuery, Result<AutoSkola.Data.Models.UserRaspored>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetUserRaspored(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<Data.Models.UserRaspored>> Handle(GetUserRasporedQuery request, CancellationToken cancellationToken)
        {
            var raspored = await unitOfWork.userRasporedRepository.getById(request.id);
            if(raspored == null)
            {
                return new Result<Data.Models.UserRaspored>
                {
                    Errors = new List<string> { $"Odsustvo za korisnika pod tim id nije pronadjeno" },
                    IsSuccess = false
                };
            }
            return new Result<Data.Models.UserRaspored>
            {
                Data = raspored
            };
        }
    }
}
