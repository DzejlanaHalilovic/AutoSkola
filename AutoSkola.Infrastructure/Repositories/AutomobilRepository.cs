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

       
    }
}
