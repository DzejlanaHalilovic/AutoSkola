using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Data.Models
{
    public class Kategorija
    {
        [Key]
        public int Id { get; set; }
        public string Tip { get; set; }

        public List<UserKategorija>userkategorija { get; set; }
        public string Putanja { get; set; }
    }
}
