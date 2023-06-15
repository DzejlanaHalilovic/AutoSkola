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
        public int Id { get; set; }
        public DateTime DatumVreme { get; set; }
        [ForeignKey(nameof(User))]
        [AllowNull]
        public int? UserId { get; set; }
        public User? User { get; set; }
        public List<UserRaspored> userraspored { get; set; }

    }
}
