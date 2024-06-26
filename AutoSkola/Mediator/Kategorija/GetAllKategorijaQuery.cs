﻿using AutoMapper;
using AutoSkola.Contracts.Models;
using AutoSkola.Contracts.Models.Kategorija;
using AutoSkola.Data.Models;
using AutoSkola.Infrastructure;
using MediatR;

namespace AutoSkola.Mediator.Kategorija
{
    public record GetAllKategorijaQuery(KategorijaFilter Request) : IRequest<Result<IEnumerable<CreateKategorijaResponse>>>
    {
    }

    public class GetKategorijaHandler : IRequestHandler<GetAllKategorijaQuery, Result<IEnumerable<CreateKategorijaResponse>>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetKategorijaHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<Result<IEnumerable<CreateKategorijaResponse>>> Handle(GetAllKategorijaQuery request, CancellationToken cancellationToken)
        {
            var lista = new List<AutoSkola.Data.Models.Kategorija>();
            //var lista = await unitOfWork.kategorijaRepository.GetAll();
            lista = await unitOfWork.kategorijaRepository.ApplyPaging(request.Request.currPage, request.Request.pageSize);
            var response = mapper.Map<IEnumerable<CreateKategorijaResponse>>(lista);
            var result = new Result<IEnumerable<CreateKategorijaResponse>>
            {
                Data = response
            };
           
            return result;
        }
    }
}
