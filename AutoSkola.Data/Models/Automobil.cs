﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Data.Models
{
    public class Automobil
    {

     [Key]
     public int RegBroj { get; set; }
     public string Model { get; set; }
    }
}
