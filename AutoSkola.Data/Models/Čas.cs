using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Data.Models
{
    public class Čas
    {
        public int Id { get; set; }
        public string Ocena { get; set; }
        [ForeignKey(nameof(Raspored))]
        [AllowNull]
        public int RasporedId { get; set; }
        public Raspored? Raspored { get; set; }

     
       

    }
}
