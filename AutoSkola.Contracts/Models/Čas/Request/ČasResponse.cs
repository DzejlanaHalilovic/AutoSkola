using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Contracts.Models.Čas.Request
{
    public class ČasResponse
    {
        public int InstruktorId { get; set; }
        public int PolaznikId { get; set; }
        public string Ocena { get; set; }

    }
}
