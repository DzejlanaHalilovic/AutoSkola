using AutoMapper;
using AutoSkola.Contracts.Models.Čas.Request;
using AutoSkola.Contracts.Models;
using AutoSkola.Data.Models;
using AutoSkola.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;

public record CreateCasCommand(CreateCasRequest casRequest) : IRequest<Result<Čas>>;

public class CreateCasHandler : IRequestHandler<CreateCasCommand, Result<Čas>>
{
    private readonly UserManager<User> userManager;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;

    public CreateCasHandler(UserManager<User> userManager, IMapper mapper, IUnitOfWork unitOfWork)
    {
        this.userManager = userManager;
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<Čas>> Handle(CreateCasCommand request, CancellationToken cancellationToken)
    {
        var raspored = await unitOfWork.rasporedRepository.getById(request.casRequest.RasporedId);
        if (raspored == null)
        {
            return new Result<Čas>
            {
                Errors = new List<string> { "Invalid RasporedId" },
                IsSuccess = false
            };
        }

        
        int instruktorId = raspored.InstruktorId;

        int rasporedId = raspored.Id;

        if (instruktorId != raspored.InstruktorId)
        {
            return new Result<Čas>
            {
                Errors = new List<string> { "You are not authorized to add a mark for this Raspored" },
                IsSuccess = false
            };
        }

        var cas = new Čas
        {
            Ocena = request.casRequest.Ocena,
            RasporedId = request.casRequest.RasporedId
        };

        await unitOfWork.časRepository.Add(cas);
        var result = await unitOfWork.CompleteAsync();
        if (!result)
        {
            return new Result<Čas>
            {
                Errors = new List<string> { "Error in adding data" },
                IsSuccess = false
            };
        }

        return new Result<Čas> { Data = cas };
    }
}
