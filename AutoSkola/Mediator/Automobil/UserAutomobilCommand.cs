using AutoMapper;
using AutoSkola.Contracts.Models;
using AutoSkola.Contracts.Models.Automobil.Request;
using AutoSkola.Contracts.Models.Automobil.Response;
using AutoSkola.Contracts.Models.PolaznikInstuktor;
using AutoSkola.Data.Models;
using AutoSkola.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AutoSkola.Mediator.Automobil
{
    public record UserAutomobilCommand(CreateUserAutoRequest Request):IRequest<Result<CreateUserAutoResponse>>
    {
    }

    public class CreateUserAutoHandler : IRequestHandler<UserAutomobilCommand, Result<CreateUserAutoResponse>>
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public CreateUserAutoHandler(UserManager<User> userManager, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<CreateUserAutoResponse>> Handle(UserAutomobilCommand request, CancellationToken cancellationToken)
        {
            var instruktor = await userManager.FindByIdAsync(request.Request.InstruktorId.ToString());

            if (instruktor == null)
            {
                return new Result<CreateUserAutoResponse>
                {
                    Errors = new List<string> { "Neispravan ID za instruktora" },
                    IsSuccess = false
                };
            }

            if (!await userManager.IsInRoleAsync(instruktor, "Instuktor"))
            {
                return new Result<CreateUserAutoResponse>
                {
                    Errors = new List<string> { "Korisnik nije instruktor" },
                    IsSuccess = false
                };
            }

            var auto = new AutoSkola.Data.Models.UserAutomobil
            {
                InstruktorId = request.Request.InstruktorId,
                AutomobilId = request.Request.AutomobilId

            };
            await unitOfWork.userAutoRepository.Add(auto);
            var result = await unitOfWork.CompleteAsync();
            if (!result)
                return new Result<CreateUserAutoResponse>
                {
                    Errors = new List<string> { "greska" },
                    IsSuccess = false
                };
            var response = mapper.Map<CreateUserAutoResponse>(auto);
            return new Result<CreateUserAutoResponse>
            {
                Data = response
            };
        }
    }
}
