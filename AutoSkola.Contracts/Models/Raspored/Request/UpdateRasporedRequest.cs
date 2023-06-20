using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Contracts.Models.Raspored.Request
{
    public class UpdateRasporedRequest
    {
        //public int? Id { get; set; }
        public DateTime DatumVreme { get; set; }

        public int InstruktorId { get; set; }
       

        public int PolaznikId { get; set; }
    }
}
