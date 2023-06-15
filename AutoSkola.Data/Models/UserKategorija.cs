using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Data.Models
{
    public class UserKategorija
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        [AllowNull]
        public int? UserId { get; set; }
        [AllowNull]
        public User? User { get; set; }

        [ForeignKey(nameof(Kategorija))]
        [AllowNull]
        public int? KategorijaId { get; set; }
        [AllowNull]
        public Kategorija? Kategorija { get; set; }

    }
}
