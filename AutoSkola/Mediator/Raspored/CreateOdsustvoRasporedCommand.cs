using AutoMapper;
using AutoSkola.Contracts.Models;
using AutoSkola.Contracts.Models.Raspored.Request;
using AutoSkola.Data.Models;
using AutoSkola.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AutoSkola.Mediator.Raspored
{
    public record CreateOdsustvoRasporedCommand(CreateRasporedRequest RasporedRequest) : IRequest<Result<AutoSkola.Data.Models.Raspored>>;

    public class CreateOsustvoRasporedHandler : IRequestHandler<CreateOdsustvoRasporedCommand, Result<AutoSkola.Data.Models.Raspored>>
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public CreateOsustvoRasporedHandler(UserManager<User> userManager, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<Data.Models.Raspored>> Handle(CreateOdsustvoRasporedCommand request, CancellationToken cancellationToken)
        {
            var instruktor = await userManager.FindByIdAsync(request.RasporedRequest.InstruktorId.ToString());
            var polaznik = await userManager.FindByIdAsync(request.RasporedRequest.PolaznikId.ToString());

            var raspored = new AutoSkola.Data.Models.Raspored
            {
                DatumVreme = DateTime.Now,
                InstruktorId = instruktor.Id,
                PolaznikId = polaznik.Id
            };

            await unitOfWork.rasporedRepository.Add(raspored);
            var result = await unitOfWork.CompleteAsync();
            if (!result)
                return new Result<Data.Models.Raspored>
                {
                    Errors = new List<string> { "error in adding data" },
                    IsSuccess = false
                };

            var polaznikInstuktor = await unitOfWork.polaznikInstuktorRepository.GetPolaznikInstuktorByPolaznikId(polaznik.Id);
            if (polaznikInstuktor != null)
            {
                polaznikInstuktor.BrojCasova += 1;
                await unitOfWork.CompleteAsync();
            }

            return new Result<Data.Models.Raspored> { Data = raspored };

        }
    }
}
