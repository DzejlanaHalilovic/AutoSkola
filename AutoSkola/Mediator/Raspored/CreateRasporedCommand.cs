using AutoMapper;
using AutoSkola.Contracts.Models;
using AutoSkola.Contracts.Models.Raspored.Request;
using AutoSkola.Data.Models;
using AutoSkola.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Globalization;


namespace AutoSkola.Mediator.Raspored
{
    public record CreateRasporedCommand(CreateRasporedRequest rasporedRequest): IRequest<Result<AutoSkola.Data.Models.Raspored>>;

    public class CreateRasporedHandler : IRequestHandler<CreateRasporedCommand, Result<AutoSkola.Data.Models.Raspored>>
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public CreateRasporedHandler(UserManager<User> userManager,IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
      

        public async Task<Result<Data.Models.Raspored>> Handle(CreateRasporedCommand request, CancellationToken cancellationToken)
        {
            var instruktor = await userManager.FindByIdAsync(request.rasporedRequest.InstruktorId.ToString());
            var polaznik = await userManager.FindByIdAsync(request.rasporedRequest.PolaznikId.ToString());

            if (instruktor == null)
            {
                return new Result<AutoSkola.Data.Models.Raspored>
                {
                    Errors = new List<string> { "Neispravan ID za instruktora" },
                    IsSuccess = false
                };
            }

            if (!await userManager.IsInRoleAsync(instruktor, "Instuktor"))
            {
                return new Result<AutoSkola.Data.Models.Raspored>
                {
                    Errors = new List<string> { "Korisnik nije instruktor" },
                    IsSuccess = false
                };
            }

            if (polaznik == null)
            {
                return new Result<AutoSkola.Data.Models.Raspored>
                {
                    Errors = new List<string> { "Neispravan ID za polaznika" },
                    IsSuccess = false
                };
            }

            if (!await userManager.IsInRoleAsync(polaznik, "Polaznik"))
            {
                return new Result<AutoSkola.Data.Models.Raspored>
                {
                    Errors = new List<string> { "Korisnik nije polaznik" },
                    IsSuccess = false
                };
            }

          

            


            // Dodeljivanje instruktora i polaznika iz baze podataka rasporedu
            var raspored = new AutoSkola.Data.Models.Raspored
            {

                DatumVreme = request.rasporedRequest.DatumVreme,
                InstruktorId = instruktor.Id,
                PolaznikId = polaznik.Id
            };

            await unitOfWork.rasporedRepository.Add(raspored);
            var result = await unitOfWork.CompleteAsync();
            if (!result)
                return new Result<Data.Models.Raspored>
                {
                    Errors = new List<string> { "error in adding daata" },
                    IsSuccess = false
                };
            return new Result<Data.Models.Raspored> { Data = raspored };

        }
    }
}
