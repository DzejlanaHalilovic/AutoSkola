using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Data.Models
{
    public class UserAutomobil

    {
        public int id { get; set; }
        public int InstruktorId { get; set; }
        [ForeignKey(nameof(InstruktorId))]
        public User? Instruktor { get; set; }
        public int AutomobilId { get; set; }
        [ForeignKey(nameof(AutomobilId))]
        public Automobil? Automobil { get; set; }


    }
}
