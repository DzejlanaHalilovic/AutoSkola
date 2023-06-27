using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Data.Models
{
    public class PolaznikInstuktor
    {
        [Key]
        public int Id { get; set; }

        public int InstruktorId { get; set; }
        [ForeignKey(nameof(InstruktorId))]
        public User? Instruktor { get; set; }
        [AllowNull]

        public int PolaznikId { get; set; }
        [ForeignKey(nameof(PolaznikId))]
        public User? Polaznik { get; set; }
        public int? BrojCasova { get; set; }







    }
}
