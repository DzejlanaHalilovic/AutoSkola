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
    public class KvarRepository : Repository<Kvar>, IKvarRepository
    {
        private readonly DataContext context;

        public KvarRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
        }
        public async Task<Kvar> GetByAutomobilId(int automobilId)
        {
            return await context.Set<Kvar>()
                .FirstOrDefaultAsync(k => k.idkvara == automobilId);
        }
    }
}
