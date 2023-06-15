using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Data.Models
{
    public class Kvar
    {
        public int Id { get; set; }
        public string Opis { get; set; }
        public DateTime DatumKvara { get; set; }
        [ForeignKey(nameof(Automobil))]
        [AllowNull]
        public int? AutomobilRegBroj { get; set; }
        [AllowNull]
        public Automobil? Automobil { get; set; }
    }
}
