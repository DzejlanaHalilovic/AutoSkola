using AutoSkola.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Contracts.Models.Automobil.Request
{
    public class CreateUserAutoRequest
    {
        public int InstruktorId { get; set; }
      
        public int AutomobilId { get; set; }
        
    }
}
