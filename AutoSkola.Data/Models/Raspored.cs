using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Data.Models
{
    public class Raspored
    {
        //public int Id { get; set; }
        //public DateTime DatumVreme { get; set; }

        //public int InstruktorId { get; set; }
        //[ForeignKey(nameof(InstruktorId))]
        //public User Instruktor { get; set; }

        //public int PolaznikId { get; set; }
        //[ForeignKey(nameof(PolaznikId))]
        //public User Polaznik { get; set; }

        //public List<UserRaspored> userraspored { get; set; }
        public int Id { get; set; }
        public DateTime DatumVreme { get; set; }

        public int InstruktorId { get; set; }
        [ForeignKey(nameof(InstruktorId))]
        public User Instruktor { get; set; }

        public int PolaznikId { get; set; }
        [ForeignKey(nameof(PolaznikId))]
        public User Polaznik { get; set; }

        public List<UserRaspored> UserRaspored { get; set; }

    }

}
