using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Contracts.Models.UserRaspored
{
    public class UserRasporedRequest
    {
        public int UserId { get; set; }
       
        public int RasporedId { get; set; }
        
        public string Razlog { get; set; }
        [DataType(DataType.Date)]
        public DateTime DatumOdsustava { get; set; }

    }
}
