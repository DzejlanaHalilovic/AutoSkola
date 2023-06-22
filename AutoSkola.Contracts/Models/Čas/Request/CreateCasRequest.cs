using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Contracts.Models.Čas.Request
{
    public class CreateCasRequest
    {
        public float Ocena { get; set; }
        public int RasporedId { get; set; }
        public int AutomobilRegBroj { get; set; }
    }
}
