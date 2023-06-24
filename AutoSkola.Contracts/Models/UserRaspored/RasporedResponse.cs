using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Contracts.Models.UserRaspored
{
    public class RasporedResponse
    {
        public int UserId { get; set; }

         public int RasporedId { get; set; }

        public string Razlog { get; set; }
        [DataType(DataType.Date)]
        public DateTime DatumOdsustava { get; set; }
    }
}
