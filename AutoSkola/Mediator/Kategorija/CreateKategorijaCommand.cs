using AutoMapper;
using AutoSkola.Contracts.Models;
using AutoSkola.Contracts.Models.Kategorija;
using AutoSkola.Data.Models;
using AutoSkola.Infrastructure;
using MediatR;
using NBP_projekat.ImageUploadMethod;

namespace AutoSkola.Mediator.Kategorija
{
    public record CreateKategorijaCommand (CreateKategorijaRequest kategorijaRequest) :IRequest<Result<CreateKategorijaResponse>>;

    public class CreateKategorijaHandler : IRequestHandler<CreateKategorijaCommand, Result<CreateKategorijaResponse>>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;

        public CreateKategorijaHandler(IMapper mapper, IUnitOfWork unitOfWork, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.hostingEnvironment = hostingEnvironment;
        }
        public async Task<Result<CreateKategorijaResponse>> Handle(CreateKategorijaCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.kategorijaRequest.Tip))
                return new Result<CreateKategorijaResponse>
                {
                    Errors = new List<string> { "Tip is required" },
                    IsSuccess = false
                };

            var newKategorija = new AutoSkola.Data.Models.Kategorija
            {
                Tip = request.kategorijaRequest.Tip,
                Putanja = await Upload.SaveFile(hostingEnvironment.ContentRootPath, request.kategorijaRequest.Putanja, "images")
            };

            await unitOfWork.kategorijaRepository.Add(newKategorija);
            var result = await unitOfWork.CompleteAsync();
            if (!result)
                return new Result<CreateKategorijaResponse>
                {
                    Errors = new List<string> { "Something is not correct with requets data" },
                    IsSuccess = false
                };
            var response = mapper.Map<CreateKategorijaResponse>(newKategorija);
            return new Result<CreateKategorijaResponse> { Data = response };


        }
    }

}
