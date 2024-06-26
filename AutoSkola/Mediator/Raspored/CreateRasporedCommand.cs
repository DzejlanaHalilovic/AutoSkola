﻿using AutoMapper;
using AutoSkola.Contracts.Models;
using AutoSkola.Contracts.Models.Raspored.Request;
using AutoSkola.Data.Models;
using AutoSkola.Infrastructure;
using AutoSkola.Infrastructure.Repositories;
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
                    Errors = new List<string> { "error in adding data" },
                    IsSuccess = false
                };

            var polaznikInstuktor = await unitOfWork.polaznikInstuktorRepository.GetPolaznikInstuktorByPolaznikId(polaznik.Id);
            if (polaznikInstuktor != null)
            {
                polaznikInstuktor.BrojCasova -= 1; 
                await unitOfWork.CompleteAsync(); 
            }

            return new Result<Data.Models.Raspored> { Data = raspored };
        }

    }
}
