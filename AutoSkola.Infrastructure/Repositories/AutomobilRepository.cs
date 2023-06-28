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
    public class AutomobilRepository : Repository<Automobil>, IAutomobilRepository
    {
        public AutomobilRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
            Context = context;
        }

        public DataContext Context { get; }

        public async Task<IEnumerable<Automobil>> GetByUserId(int userId)
        {
            var kategorijaIds = await Context.userkategorija
                .Where(uk => uk.UserId == userId)
                .Select(uk => uk.KategorijaId)
                .ToListAsync();

            var dodeljeniAutomobili = await Context.userAutomobil
                .Where(ua => ua.InstruktorId != null)
                .Select(ua => ua.AutomobilId)
                .ToListAsync();

            return await Context.automobili
                .Where(a => kategorijaIds.Contains(a.KategorijaId) && !dodeljeniAutomobili.Contains(a.Id))
                .ToListAsync();
        }


    }
}
