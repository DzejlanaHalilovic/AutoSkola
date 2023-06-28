using AutoSkola.Contracts.Models;
using AutoSkola.Infrastructure;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AutoSkola.Mediator.Automobil
{
    public class GetAutaKategorijaQuery : IRequest<Result<IEnumerable<Data.Models.Automobil>>>
    {
        public int KategorijaId { get; set; }

        public GetAutaKategorijaQuery(int kategorijaId)
        {
            KategorijaId = kategorijaId;
        }
    }

    public class GetAutoKategorijaHandler : IRequestHandler<GetAutaKategorijaQuery, Result<IEnumerable<Data.Models.Automobil>>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetAutoKategorijaHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<Data.Models.Automobil>>> Handle(GetAutaKategorijaQuery request, CancellationToken cancellationToken)
        {
            return null;
        }

    }
}
