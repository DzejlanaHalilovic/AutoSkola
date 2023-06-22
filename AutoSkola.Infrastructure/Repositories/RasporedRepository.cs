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
    public class RasporedRepository : Repository<Raspored>, IRasporedRepository
    {
        private readonly DataContext context;

        public RasporedRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
        }
        public async Task<int> GetPolazniciCountByInstruktorId(int instruktorId)
        {
            return await context.raspored
                .CountAsync(raspored => raspored.InstruktorId == instruktorId);
        }

        public async Task<List<Raspored>> GetRasporediByPolaznikId(int polaznikId)
        {
            return await context.raspored
                .Where(r => r.PolaznikId == polaznikId)
                .ToListAsync();
        }

    }
}
