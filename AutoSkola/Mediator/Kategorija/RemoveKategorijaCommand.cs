using AutoSkola.Contracts.Models;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.Kategorija
{
    public record RemoveKategorijaCommand(int id):IRequest<Result<bool>>
    {

    }

    public class RemoveKategorijaHandler : IRequestHandler<RemoveKategorijaCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;

        public RemoveKategorijaHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(RemoveKategorijaCommand request, CancellationToken cancellationToken)
        {
            var kategorija = await unitOfWork.kategorijaRepository.getById(request.id);
            if(kategorija == null)
            {
                return new Result<bool>
                {
                    Errors = new List<string> { "Kategorija nije pronadjena" },
                    IsSuccess = false
                };
                
            }
            var result = await unitOfWork.kategorijaRepository.Delete(kategorija);
            if (!result)
                return new Result<bool>
                {
                    IsSuccess = false
                };
            return new Result<bool>
            {
                IsSuccess = true
            };
        }
    }
}
