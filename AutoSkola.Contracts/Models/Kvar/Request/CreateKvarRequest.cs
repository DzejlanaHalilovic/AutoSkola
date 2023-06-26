using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Contracts.Models.Kvar.Request
{
    public class CreateKvarRequest
    {
       
        public string Opis { get; set; }
        [DataType(DataType.Date)]
        public DateTime DatumKvara { get; set; }
      
       
        public int Id { get; set; }
      
        

    }
}
