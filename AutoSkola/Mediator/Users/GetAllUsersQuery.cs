using AutoMapper;
using AutoSkola.Contracts.Models;
using AutoSkola.Contracts.Models.Identity.Response;
using AutoSkola.Data.Models;
using AutoSkola.Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AutoSkola.Mediator.Users
{
    public record GetAllUsersQuery : IRequest<Result<IEnumerable<UserResponse>>> { }

    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, Result<IEnumerable<UserResponse>>>
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly IKategorijaRepository kategorijaRepository;

        public GetAllUsersHandler(UserManager<User> userManager, IMapper mapper, IKategorijaRepository kategorijaRepository)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.kategorijaRepository = kategorijaRepository;
        }

        public async Task<Result<IEnumerable<UserResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var lista = await userManager.Users.Include(u => u.userkategorija).ToListAsync();
            var mappedList = mapper.Map<List<UserResponse>>(lista);

            foreach (var userResponse in mappedList)
            {
                var kategorijaId = userResponse.kategorijaId;
                var kategorija = await kategorijaRepository.GetKategorijaByIdAsync(kategorijaId);
                userResponse.TipKategorije = kategorija?.Tip ?? string.Empty;
            }

            return new Result<IEnumerable<UserResponse>>
            {
                Data = mappedList
            };
        }
    }
}
