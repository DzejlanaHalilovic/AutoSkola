using AutoSkola.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Contracts.Models.PolaznikInstuktor
{
    public class PolaznikInstuktorRequest
    {
        public int InstruktorId { get; set; }
      

        public int PolaznikId { get; set; }
        public int BrojCasova { get; set; }
    }
}
