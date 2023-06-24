using AutoMapper;
using AutoSkola.Contracts.Models;
using AutoSkola.Contracts.Models.PolaznikInstuktor;
using AutoSkola.Data.Models;
using AutoSkola.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AutoSkola.Mediator.PolaznikInstuktor
{
    public record PolaznikInstuktorCommand(PolaznikInstuktorRequest Request):IRequest<Result<PolaznikInstuktorResponse>>
    {
    }

    public class CreatePolaznikInstuktor : IRequestHandler<PolaznikInstuktorCommand, Result<PolaznikInstuktorResponse>>
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public CreatePolaznikInstuktor(UserManager<User> userManager, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async  Task<Result<PolaznikInstuktorResponse>> Handle(PolaznikInstuktorCommand request, CancellationToken cancellationToken)
        {
            var instruktor = await userManager.FindByIdAsync(request.Request.InstruktorId.ToString());
            var polaznik = await userManager.FindByIdAsync(request.Request.PolaznikId.ToString());
            if (instruktor == null)
            {
                return new Result<PolaznikInstuktorResponse>
                {
                    Errors = new List<string> { "Neispravan ID za instruktora" },
                    IsSuccess = false
                };
            }

            if (!await userManager.IsInRoleAsync(instruktor, "Instuktor"))
            {
                return new Result<PolaznikInstuktorResponse>
                {
                    Errors = new List<string> { "Korisnik nije instruktor" },
                    IsSuccess = false
                };
            }

            if (polaznik == null)
            {
                return new Result<PolaznikInstuktorResponse>
                {
                    Errors = new List<string> { "Neispravan ID za polaznika" },
                    IsSuccess = false
                };
            }

            if (!await userManager.IsInRoleAsync(polaznik, "Polaznik"))
            {
                return new Result<PolaznikInstuktorResponse>
                {
                    Errors = new List<string> { "Korisnik nije polaznik" },
                    IsSuccess = false
                };
            }
            var instruktorPolazniciCount = await unitOfWork.polaznikInstuktorRepository.GetPolazniciCountByInstruktorId(instruktor.Id);
            if (instruktorPolazniciCount >= 3)
            {
                return new Result<PolaznikInstuktorResponse>
                {
                    Errors = new List<string> { "Instruktor već ima maksimalan broj polaznika" },
                    IsSuccess = false
                };
            }

            // Provera da li instruktor već ima dodatog polaznika
            var existingPolaznikInstuktor = await unitOfWork.polaznikInstuktorRepository.GetPolaznikInstuktorByPolaznikIdAndInstruktorId(polaznik.Id, instruktor.Id);
            if (existingPolaznikInstuktor != null)
            {
                return new Result<PolaznikInstuktorResponse>
                {
                    Errors = new List<string> { "Polaznik je već dodat instruktoru" },
                    IsSuccess = false
                };
            }

            var korisnici = new AutoSkola.Data.Models.PolaznikInstuktor
            {
                InstruktorId = request.Request.InstruktorId,
                PolaznikId = request.Request.PolaznikId
            };
            await unitOfWork.polaznikInstuktorRepository.Add(korisnici);
            var result = await unitOfWork.CompleteAsync();
            if (!result)
                return new Result<PolaznikInstuktorResponse>
                {
                    Errors = new List<string> { "greska" },
                    IsSuccess = false
                };
            var response = mapper.Map<PolaznikInstuktorResponse>(korisnici);
            return new Result<PolaznikInstuktorResponse>
            {
                Data = response
            };




        }
    }
}
