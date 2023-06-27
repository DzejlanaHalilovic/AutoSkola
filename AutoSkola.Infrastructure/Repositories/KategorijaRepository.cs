using AutoMapper;
using AutoSkola.Contracts.Models.Extensions;
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
    public class KategorijaRepository : Repository<Kategorija>, IKategorijaRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public KategorijaRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Kategorija> GetKategorijaByIdAsync(int kategorijaId)
        {
            return await context.kategorije.FindAsync(kategorijaId);
        }
        public async Task<Kategorija> GetUserKategorija(int userId)
        {
            var user = await context.Users
                .Include(u => u.userkategorija)
                .ThenInclude(uk => uk.Kategorija)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user?.userkategorija?.FirstOrDefault().Kategorija;
        }

        public async Task<List<Kategorija>> ApplyPaging(int currPage, int pageSize)
        {
            var query = (await context.kategorije.ToListAsync()).AsQueryable();
            query = query.ApplyPaging(currPage, pageSize);
            if (query == null)
                return null;
            var listToShow = mapper.Map<List<Kategorija>>(query.ToList());
            return listToShow;

        }

    }
}

