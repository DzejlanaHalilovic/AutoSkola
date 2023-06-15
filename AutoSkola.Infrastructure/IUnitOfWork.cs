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
        Task<bool> CompleteAsync();
        IAutomobilRepository automobilRepository { get; }
        IČasRepository časRepository { get; }
        IKategorijaRepository kategorijaRepository { get; }
        IKvarRepository kvarRepository { get; }
        IRasporedRepository rasporedRepository { get; }
        IUserRepository userRepository { get; }
    }
}
