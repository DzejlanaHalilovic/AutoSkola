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
    public class UserRaspored
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        [AllowNull]
        public int? UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey(nameof(Raspored))]
        [AllowNull]
       // public int? RasporedId { get; set; }

        //public Raspored? Raspored { get; set; }
        public string Razlog { get; set; }
        public DateTime DatumOdsustava { get; set; }
    }
}
