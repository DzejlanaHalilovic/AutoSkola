using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Contracts.Models.Automobil.Request
{
    public class CreateAutomobilRequest
    {
        public int RegBroj { get; set; }
        public string Model { get; set; }
        public int KategorijaId { get; set; }
    }
}
