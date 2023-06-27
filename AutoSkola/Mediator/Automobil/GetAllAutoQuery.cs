using AutoSkola.Contracts.Models;
using AutoSkola.Infrastructure;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AutoSkola.Mediator.Automobil
{
    public class GetAllAutoQuery : IRequest<Result<IEnumerable<Data.Models.Automobil>>>
    {

    }

    public class GetAllHandler : IRequestHandler<GetAllAutoQuery, Result<IEnumerable<Data.Models.Automobil>>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetAllHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<IEnumerable<Data.Models.Automobil>>> Handle(GetAllAutoQuery request, CancellationToken cancellationToken)
        {
            var automobili = await unitOfWork.automobilRepository.GetAll();

            var automobiliBezKvara = new List<Data.Models.Automobil>();

            foreach (var automobil in automobili)
            {
                var kvar = await unitOfWork.kvarRepository.GetByAutomobilId(automobil.Id);

                if (kvar == null)
                {
                    automobiliBezKvara.Add(automobil);
                }
            }

            return new Result<IEnumerable<Data.Models.Automobil>>
            {
                Data = automobiliBezKvara
            };
        }
    }
}
