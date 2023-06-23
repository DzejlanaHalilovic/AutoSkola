using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Contracts.Models.Identity.Request
{
    public class RegisterRequest
    {
       
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        [DataType(DataType.Date)]
        public DateTime DatumRodjenja { get; set; }
        public char Pol { get; set; }
        public string BrojTelefona { get; set; }
        public int Role { get; set; }
        //public string userraspored { get; set; }
        // public int kategorijaId { get; set; }
       public int kategorijaId { get; set; }

    }
}
