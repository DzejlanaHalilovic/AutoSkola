﻿using AutoSkola.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Infrastructure.Interfaces
{
    public  interface IKvarRepository : IRepository<Kvar>
    {
        Task<Kvar> GetByAutomobilId(int automobilId);
    }
}
