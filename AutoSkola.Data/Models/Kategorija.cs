using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Data.Models
{
    public class Kategorija
    {
        public int Id { get; set; }
        public string Tip { get; set; }

        public List<UserKategorija>userkategorija { get; set; }
    }
}
