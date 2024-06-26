﻿using AutoMapper;
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
        
        public async Task<List<Raspored>> GetRasporediByPolaznikId(int polaznikId)
        {
            return await context.raspored
                .Where(r => r.PolaznikId == polaznikId)
                .ToListAsync();
        }
        public async Task<List<Raspored>> GetByInstruktorIdAsync(int instruktorId)
        {
            return await context.Set<Raspored>()
                .Where(pi => pi.InstruktorId == instruktorId)
                .ToListAsync();
        }
       
        public async Task<List<Raspored>>getrasporedzapolaznika(int idusera)
        {
            var list = await context.raspored.Where(x => x.PolaznikId == idusera).ToListAsync();
            if (list == null)
                return null;
            return list;
        }
        public async Task<List<Raspored>> getrasporedzaintukora(int idusera)
        {
            var list = await context.raspored.Where(x => x.InstruktorId == idusera).ToListAsync();
            if (list == null)
                return null;
            return list;
        }
        public async Task<List<Raspored>> GetTop15RasporedaZaInstruktora(int id)
        {
            var result = await context.raspored
                .Where(x => x.InstruktorId == id)
                .OrderByDescending(x => x.DatumVreme)
                .Take(15)
                .ToListAsync();

            return result;
        }

        public async Task<List<Raspored>> GetFilteredRasporedaZaInstruktora(int id, DateTime? datum)
{
            var query = context.raspored.Where(x => x.InstruktorId == id);

            if (datum.HasValue)
            {
                // Filtriraj rasporede po unetom datumu
                query = query.Where(x => x.DatumVreme.Date == datum.Value.Date);
            }

            var result = await query.ToListAsync();
            return result;
}

    }
}
