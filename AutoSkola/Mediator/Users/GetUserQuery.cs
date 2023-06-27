using AutoMapper;
using AutoSkola.Contracts.Models;
using AutoSkola.Contracts.Models.Identity.Response;
using AutoSkola.Data.Models;
using AutoSkola.Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AutoSkola.Mediator.Users
{
    public record GetUserQuery(int id) : IRequest<Result<UserResponse>>
    {
    }

    public class GetUserHandler : IRequestHandler<GetUserQuery, Result<UserResponse>>
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly IKategorijaRepository kategorijaRepository;

        public GetUserHandler(UserManager<User> userManager, IMapper mapper, IKategorijaRepository kategorijaRepository)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.kategorijaRepository = kategorijaRepository;
        }

        public async Task<Result<UserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.Users.Include(u => u.userkategorija).FirstOrDefaultAsync(u => u.Id == request.id);

            var mappedUser = mapper.Map<UserResponse>(user);

            var kategorijaId = user?.userkategorija?.FirstOrDefault()?.KategorijaId;
            if (kategorijaId != null)
            {
                var kategorija = await kategorijaRepository.GetKategorijaByIdAsync(kategorijaId.Value);
                mappedUser.TipKategorije = kategorija?.Tip;
            }
            if (user == null)
            {
                return new Result<UserResponse>
                {
                    Errors = new List<string> { $"User with ID {request.id} is not found" },
                    IsSuccess = false
                };
            }

            //var mappedUser = mapper.Map<UserResponse>(user);

            //var kategorijaId = user.userkategorija?.FirstOrDefault().KategorijaId;
            //if (kategorijaId != null)
            //{
                //var kategorija = await kategorijaRepository.GetKategorijaByIdAsync(kategorijaId.Value);
               // mappedUser.TipKategorije = kategorija?.Tip;
           // }

            return new Result<UserResponse>
            {
                Data = mappedUser
            };
        }
    }
}
