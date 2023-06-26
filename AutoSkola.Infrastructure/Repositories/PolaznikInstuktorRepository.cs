using AutoMapper;
using AutoSkola.Data;
using AutoSkola.Data.Models;
using AutoSkola.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Infrastructure.Repositories
{
    public class PolaznikInstuktorRepository : Repository<PolaznikInstuktor>, IPolaznikInstuktorRepository
    {
        private readonly DataContext context;

        public PolaznikInstuktorRepository(DataContext context,IMapper mapper) :base(context,mapper)
        {
            this.context = context;
        }
        public async Task<int> GetPolazniciCountByInstruktorId(int instruktorId)
        {
            return await context.polaznikinstuktor
                .CountAsync(polaznikinstuktor => polaznikinstuktor.InstruktorId == instruktorId);
        }
        public async Task<PolaznikInstuktor> GetPolaznikInstuktorByPolaznikIdAndInstruktorId(int polaznikId, int instruktorId)
        {
            return await context.polaznikinstuktor
                .FirstOrDefaultAsync(pi => pi.PolaznikId == polaznikId && pi.InstruktorId == instruktorId);
        }

        public async Task<List<User>> GetInstruktoriByKategorija(int kategorijaId)
        {
            var instruktori = await context.Users
                .Include(u => u.userkategorija)
                .Where(u => u.userkategorija.Any(uk => uk.KategorijaId == kategorijaId))
                .ToListAsync();

            return instruktori;
        }
        public async Task<int> GetBrojPolaznikaByInstruktorId(int instruktorId)
        {
            return await context.polaznikinstuktor
                .CountAsync(pi => pi.InstruktorId == instruktorId);
        }



    }
}
