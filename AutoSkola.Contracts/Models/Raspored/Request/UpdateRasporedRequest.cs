using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Contracts.Models.Raspored.Request
{
    public class UpdateRasporedRequest
    {
        public DateTime DatumVreme { get; set; }

        public int? UserId { get; set; }
    }
}
