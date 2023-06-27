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
            var userList = new List<UserResponse>();

            var users = await userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                var userWithKategorija = await userManager.Users.Include(u => u.userkategorija)
                                                              .FirstOrDefaultAsync(u => u.Id == user.Id);

                var mappedUser = mapper.Map<UserResponse>(userWithKategorija);

                var kategorijaId = userWithKategorija?.userkategorija?.FirstOrDefault()?.KategorijaId;
                if (kategorijaId != null)
                {
                    var kategorija = await kategorijaRepository.GetKategorijaByIdAsync(kategorijaId.Value);
                    mappedUser.TipKategorije = kategorija?.Tip;
                }

                userList.Add(mappedUser);
            }

            if (userList.Count == 0)
            {
                return new Result<IEnumerable<UserResponse>>
                {
                    Errors = new List<string> { "There are no users" },
                    IsSuccess = false
                };
            }

            return new Result<IEnumerable<UserResponse>>
            {
                Data = userList
            };
        }



    }
}
