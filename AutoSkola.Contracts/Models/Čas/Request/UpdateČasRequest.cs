using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Contracts.Models.Čas.Request
{
    public class UpdateČasRequest
    {
        public int RasporedId { get; set; }
        public int AutomobilRegBroj { get; set; }

    }
}
