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
    public class ČasRepository : Repository<Čas>, IČasRepository
    {
        private readonly DataContext context;

        public ČasRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
        }

        public async Task<List<Čas>> GetCasoviByRasporedId(int rasporedId)
        {
            return await context.casovi
                .Where(c => c.RasporedId == rasporedId)
                .ToListAsync();
        }


    }
}
