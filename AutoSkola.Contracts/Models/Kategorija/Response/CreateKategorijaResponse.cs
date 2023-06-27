using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Contracts.Models.Kategorija.Response
{
    public class CreateKategorijaResponse
    {
        public int Id { get; set; }
        public string Tip { get; set; }
        public string Putanja { get; set; }

    }
}
