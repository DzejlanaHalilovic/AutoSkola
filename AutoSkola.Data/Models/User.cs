using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Data.Models
{
    public class User : IdentityUser<int>
    {
       
        [Required]
        [MaxLength(10)]
        public string Ime { get; set; }
        [Required]
        [MaxLength(15)]
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public char Pol { get; set; }
        public string BrojTelefona { get; set; }
        public List<UserKategorija>userkategorija { get; set; }
        public List<UserRaspored>userraspored { get; set; }
    }
}
