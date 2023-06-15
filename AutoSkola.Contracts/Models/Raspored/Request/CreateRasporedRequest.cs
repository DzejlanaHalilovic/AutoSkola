using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Contracts.Models.Raspored.Request
{
    public class CreateRasporedRequest
    {
        public DateTime DatumVreme { get; set; }
      
        public int? UserId { get; set; }
       

    }
}
