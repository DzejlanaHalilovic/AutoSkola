using AutoMapper;
using AutoSkola.Data;
using AutoSkola.Data.Models;
using AutoSkola.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Infrastructure.Repositories
{
    public class KategorijaRepository : Repository<Kategorija>, IKategorijaRepository
    {
        private readonly DataContext context;

        public KategorijaRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
        }

        public async Task<Kategorija> GetKategorijaByIdAsync(int kategorijaId)
        {
            return await context.kategorije.FindAsync(kategorijaId);
        }
    }
}
