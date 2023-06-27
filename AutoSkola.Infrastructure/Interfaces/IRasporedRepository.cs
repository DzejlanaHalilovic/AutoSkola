using AutoSkola.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Infrastructure.Interfaces
{
    public interface IRasporedRepository : IRepository<Raspored>
    {
       
        Task<List<Raspored>> GetRasporediByPolaznikId(int polaznikId);
        Task<List<Raspored>> GetByInstruktorIdAsync(int instruktorId);
        Task<List<Raspored>> getrasporedzapolaznika(int idusera);
        Task<List<Raspored>> getrasporedzaintukora(int idusera);
    }
}
