using AutoSkola.Contracts.Models;
using AutoSkola.Data;
using AutoSkola.Data.Models;
using AutoSkola.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AutoSkola.Mediator.Users
{
    public record DeleteUserCommand(int id) : IRequest<Result<bool>>
    {
    }

    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Result<bool>>
    {
        private readonly UserManager<User> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly DataContext context;

        public DeleteUserHandler(UserManager<User> userManager, IUnitOfWork unitOfWork,DataContext context)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.context = context;
        }

        public async Task<Result<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.id.ToString());
            if (user == null)
            {
                return new Result<bool>
                {
                    Errors = new List<string> { $"Korisnik sa ID {request.id} nije pronađen" },
                    IsSuccess = false
                };
            }

            // Pronalaženje rasporeda koji pripadaju korisniku
            var rasporedi = await context.raspored
                .Where(r => r.InstruktorId == user.Id || r.PolaznikId == user.Id)
                .ToListAsync();

            // Brisanje rasporeda
            context.raspored.RemoveRange(rasporedi);

            // Pronalaženje polaznika i instruktora koji pripadaju korisniku
            var polazniciInstruktori = await context.polaznikinstuktor
                .Where(pi => pi.InstruktorId == user.Id || pi.PolaznikId == user.Id)
                .ToListAsync();

            // Brisanje polaznika i instruktora
            context.polaznikinstuktor.RemoveRange(polazniciInstruktori);

            // Brisanje korisnika
            context.Users.Remove(user);

            // Čekanje na završetak prethodnih operacija nad bazom podataka
            await context.SaveChangesAsync();

            return new Result<bool>
            {
                IsSuccess = true
            };
        }

    }
}
