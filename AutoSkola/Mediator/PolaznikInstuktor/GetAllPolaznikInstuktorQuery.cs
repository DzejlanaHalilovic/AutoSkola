using AutoMapper;
using AutoSkola.Contracts.Models;
using AutoSkola.Contracts.Models.PolaznikInstuktor;
using AutoSkola.Data.Models;
using AutoSkola.Infrastructure;
using AutoSkola.Mediator.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AutoSkola.Mediator.PolaznikInstuktor
{
    public class GetAllPolaznikInstuktorQuery : IRequest<Result<IEnumerable<PolaznikInstuktorResponse>>>
    {
    }

    public class GetAllPolaznikInstuktorHandler : IRequestHandler<GetAllPolaznikInstuktorQuery, Result<IEnumerable<PolaznikInstuktorResponse>>>
    {
        private readonly UserManager<User> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllPolaznikInstuktorHandler(UserManager<User> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<IEnumerable<PolaznikInstuktorResponse>>> Handle(GetAllPolaznikInstuktorQuery request, CancellationToken cancellationToken)
        {
            var lista = await unitOfWork.polaznikInstuktorRepository.GetAll();
            var response = new List<PolaznikInstuktorResponse>();

            foreach (var item in lista)
            {
                var instruktor = await userManager.FindByIdAsync(item.InstruktorId.ToString());
                var polaznik = await userManager.FindByIdAsync(item.PolaznikId.ToString());

                var polaznikInstuktorResponse = new PolaznikInstuktorResponse
                {
                    InstruktorId = item.InstruktorId,
                    InstruktorIme = instruktor?.Ime,
                    InstruktorPrezime = instruktor?.Prezime,
                    PolaznikId = item.PolaznikId,
                    PolaznikIme = polaznik?.Ime,
                    PolaznikPrezime = polaznik?.Prezime,
                };

                response.Add(polaznikInstuktorResponse);
            }

            var result = new Result<IEnumerable<PolaznikInstuktorResponse>>
            {
                Data = response
            };

            return result;
        }
    }

}

