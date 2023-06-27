using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Contracts.Models.Kategorija
{
    public class CreateKategorijaRequest
    {
        public string Tip { get; set; }
        public IFormFile Putanja { get; set; }
       
    }
}
