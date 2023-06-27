using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Contracts.Models.PolaznikInstuktor
{
    public class PolaznikInstuktorResponse
    {
        public int InstruktorId { get; set; }
        public string InstruktorIme { get; set; }
        public string InstruktorPrezime { get; set; }
        public string PolaznikIme { get; set; }
        public string PolaznikPrezime { get; set; }
        public int PolaznikId { get; set; }
        public int BrojCasova { get; set; }

    }
}
