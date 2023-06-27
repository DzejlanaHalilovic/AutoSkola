using AutoSkola.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Infrastructure
{
    public interface IUnitOfWork
    {

        IAutomobilRepository automobilRepository { get; }
        IČasRepository časRepository { get; }
        IKategorijaRepository kategorijaRepository { get; }
        IKvarRepository kvarRepository { get; }
        IRasporedRepository rasporedRepository { get; }
        IUserRepository userRepository { get; }
        IUserRasporedRepository userRasporedRepository { get; } 
        IPolaznikInstuktorRepository polaznikInstuktorRepository { get; }
        Task<bool> CompleteAsync();

        
    }
}
