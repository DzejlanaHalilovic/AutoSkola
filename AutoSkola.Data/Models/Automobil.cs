using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Data.Models
{
    public class Automobil
    {
     [Key]   public int Id { get; set; }
        public string RegBroj { get; set; }
        public string Model { get; set; } 
        [ForeignKey(nameof(KategorijaId))]
        [AllowNull]
        public int KategorijaId { get; set; }
       
        public Kategorija Kategorija { get; set; }
       
        


    }
}
