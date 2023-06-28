using AutoSkola.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Infrastructure.Interfaces
{
    public interface IČasRepository : IRepository<Čas>
    {
        Task<List<Čas>> GetCasoviByRasporedId(int rasporedId);
        //Task<bool> AddMark(Čas request);
        Task<List<Čas>> GetOceneByPolaznikId(int polaznikId);
    }
}
