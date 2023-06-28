using AutoMapper;
using AutoSkola.Contracts.Models;
using AutoSkola.Contracts.Models.Automobil.Request;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.Automobil
{
    public record CreateAutoCommand(CreateAutomobilRequest request) : IRequest<Result<AutoSkola.Data.Models.Automobil>>
    {
    }

    public class CreateAutoHandler : IRequestHandler<CreateAutoCommand, Result<AutoSkola.Data.Models.Automobil>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateAutoHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<Result<Data.Models.Automobil>> Handle(CreateAutoCommand request, CancellationToken cancellationToken)
        {
            if (request.request.Model == null && request.request.RegBroj == null)
                return new Result<Data.Models.Automobil>
                {
                    Errors = new List<string> { "Obavezna polja" },
                    IsSuccess = false
                };
            var auto = new Data.Models.Automobil
            {
                Model = request.request.Model,
                RegBroj = request.request.RegBroj.ToString(),  
                KategorijaId = request.request.KategorijaId
            };
            await unitOfWork.automobilRepository.Add(auto);
            var result = await unitOfWork.CompleteAsync();
            if (!result)
                return new Result<Data.Models.Automobil>
                {
                    Errors = new List<string> { "error in adding data" },
                    IsSuccess = false
                };
            return new Result<Data.Models.Automobil>
            {
                Data = auto
            };
        }
    }

}
