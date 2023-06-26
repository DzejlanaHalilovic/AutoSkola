using AutoMapper;
using AutoSkola.Contracts.Models;
using AutoSkola.Contracts.Models.Raspored.Request;
using AutoSkola.Contracts.Models.UserRaspored;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.UserRaspored
{
    public record CreateUserRasporedCommand (UserRasporedRequest request):IRequest<Result<AutoSkola.Data.Models.UserRaspored>>;

    public class CreateUserRasporedHandler : IRequestHandler<CreateUserRasporedCommand, Result<AutoSkola.Data.Models.UserRaspored>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateUserRasporedHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<Result<Data.Models.UserRaspored>> Handle(CreateUserRasporedCommand request, CancellationToken cancellationToken)
        {
            if (request.request.DatumOdsustava == null)
                return new Result<Data.Models.UserRaspored>
                {
                    Errors = new List<string> { "Datum je obavezno polje" },
                    IsSuccess = false
                };
            //var raspored = await unitOfWork.rasporedRepository.getById(request.request.RasporedId);
            //if (raspored == null)
            //    return new Result<Data.Models.UserRaspored>
            //    {
            //        Errors = new List<string> { "Raspored nije pronađen" },
            //        IsSuccess = false
            //    };

            var user = (await unitOfWork.userRepository.GetAll())
                .Where(u => u.Id == request.request.UserId)
                .FirstOrDefault();

            if (user == null)
                return new Result<Data.Models.UserRaspored>
                {
                    Errors = new List<string> { "Korisnik nije pronađen" },
                    IsSuccess = false
                };
            //var rasporedi = await unitOfWork.rasporedRepository.getById(request.request.RasporedId);
            //if (rasporedi == null || (raspored.InstruktorId != request.request.UserId && raspored.PolaznikId != request.request.UserId))
            //{
            //    return new Result<Data.Models.UserRaspored>
            //    {
            //        Errors = new List<string> { "Nemate ovlašćenje za dodavanje odsustva na ovaj raspored" },
            //        IsSuccess = false
            //    };
            //}
            

            var odsustvo = new AutoSkola.Data.Models.UserRaspored
            {
                DatumOdsustava = request.request.DatumOdsustava,
                Razlog = request.request.Razlog,
                UserId = request.request.UserId,
                //RasporedId = request.request.RasporedId
            };
            await unitOfWork.userRasporedRepository.Add(odsustvo);
            var result = await unitOfWork.CompleteAsync();
            if (!result)
                return new Result<Data.Models.UserRaspored>
                {
                    Errors = new List<string> { "Error in addning data" },
                    IsSuccess = false
                };
            return new Result<Data.Models.UserRaspored> { Data = odsustvo };

        }
    }
}
