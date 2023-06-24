using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Contracts.Models.Raspored.Response
{
    public class RasporedResponse
    {
        public DateTime DatumVreme { get; set; }

        //public TimeSpan Vreme { get; set; }
        public int InstruktorId { get; set; }

        public int PolaznikId { get; set; }
    }
}
