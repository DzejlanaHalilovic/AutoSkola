﻿using AutoSkola.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Infrastructure.Interfaces
{
    public interface IRasporedRepository : IRepository<Raspored>
    {
        Task<int> GetPolazniciCountByInstruktorId(int instruktorId);
        Task<List<Raspored>> GetRasporediByPolaznikId(int polaznikId);
    }
}
