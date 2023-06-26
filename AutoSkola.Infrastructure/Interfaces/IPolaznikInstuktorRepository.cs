using AutoSkola.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Infrastructure.Interfaces
{
    public interface IPolaznikInstuktorRepository : IRepository<PolaznikInstuktor>
    {
        Task<int> GetPolazniciCountByInstruktorId(int instruktorId);
        Task<PolaznikInstuktor> GetPolaznikInstuktorByPolaznikIdAndInstruktorId(int polaznikId, int instruktorId);
        Task<List<User>> GetInstruktoriByKategorija(int kategorijaId);
        Task<int> GetBrojPolaznikaByInstruktorId(int instruktorId);
    }
}
