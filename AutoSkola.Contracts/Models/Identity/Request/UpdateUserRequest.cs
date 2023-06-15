using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Contracts.Models.Identity.Request
{
    public class UpdateUserRequest
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public string BrojTelefona { get; set; }

    }
}
