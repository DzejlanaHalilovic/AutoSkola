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
    public class ČasRepository : Repository<Čas>, IČasRepository
    {
        public ČasRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
