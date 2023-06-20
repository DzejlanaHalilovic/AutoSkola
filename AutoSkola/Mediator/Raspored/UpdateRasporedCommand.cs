using AutoMapper;
using AutoSkola.Contracts.Models;
using AutoSkola.Contracts.Models.Raspored.Request;
using AutoSkola.Data.Models;
using AutoSkola.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AutoSkola.Mediator.Raspored
{
    public record UpdateRasporedCommand(int id, int InstruktorId,int PolaznikId, DateTime DatumVreme) : IRequest<Result<AutoSkola.Data.Models.Raspored>>
    {
    }
    public class UpdateRasporedHandler : IRequestHandler<UpdateRasporedCommand, Result<AutoSkola.Data.Models.Raspored>>
    {
        private readonly UserManager<User> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UpdateRasporedHandler(UserManager<User> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<Data.Models.Raspored>> Handle(UpdateRasporedCommand request, CancellationToken cancellationToken)
        {
          
            var instruktor = await userManager.FindByIdAsync(request.InstruktorId.ToString());
            var polaznik = await userManager.FindByIdAsync(request.PolaznikId.ToString());
      
            var rasporednovi = await unitOfWork.rasporedRepository.getById(request.id);
            if (rasporednovi == null)
                return new Result<Data.Models.Raspored>
                {
                    Errors = new List<string> { "Raspored nije pronadjen" },
                    IsSuccess = false
                };

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
            rasporednovi.InstruktorId = request.InstruktorId;
            rasporednovi.PolaznikId = request.PolaznikId;
            rasporednovi.DatumVreme = request.DatumVreme;
            var result = await unitOfWork.CompleteAsync();
            if (!result)
                return new Result<Data.Models.Raspored>
                {
                    Errors = new List<string> { "error in saving data" },
                    IsSuccess = false
                };
            return new Result<Data.Models.Raspored> { Data = rasporednovi };
          

        }
    } }
