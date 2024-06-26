﻿using AutoSkola.Contracts.Models.Kategorija;
using AutoSkola.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Contracts.Models.Identity.Response
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }


        [DataType(DataType.Date)]
        public DateTime DatumRodjenja { get; set; }  [DataType(DataType.Date)]

        public char Pol { get; set; }
        public string BrojTelefona { get; set; }
       public string? Error { get; set; }
       // public Data.Models.UserKategorija kategorijaId { get; set; }
       public int kategorijaId { get; set; }
        public string? TipKategorije { get; set; }
    }
}
