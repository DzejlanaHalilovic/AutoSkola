using AutoSkola.Data;
using AutoSkola.Data.Models;
using AutoSkola.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext context;

        public UnitOfWork(DataContext context,IAutomobilRepository automobilRepository, IČasRepository časRepository, IKategorijaRepository kategorijaRepository, IKvarRepository kvarRepository, IRasporedRepository rasporedRepository, IUserRepository userRepository, IUserRasporedRepository userRasporedRepository, IPolaznikInstuktorRepository polaznikInstuktorRepository)
        {
            this.context = context;
            this.automobilRepository = automobilRepository;
            this.časRepository = časRepository;
            this.kategorijaRepository = kategorijaRepository;
            this.kvarRepository = kvarRepository;
            this.rasporedRepository = rasporedRepository;
            this.userRepository = userRepository;
            this.userRasporedRepository = userRasporedRepository;
            this.polaznikInstuktorRepository = polaznikInstuktorRepository;
        }
        public IAutomobilRepository automobilRepository { get; }

        public IČasRepository časRepository { get; }

        public IKategorijaRepository kategorijaRepository { get; }

        public IKvarRepository kvarRepository { get; }

        public IRasporedRepository rasporedRepository { get; }

        public IUserRepository userRepository { get; }

        public IUserRasporedRepository userRasporedRepository { get; }

        public IPolaznikInstuktorRepository polaznikInstuktorRepository { get; }

        public async  Task<bool> CompleteAsync()
        {
            var result = await context.SaveChangesAsync();
            return result > 0;
        }

       
    }
}
