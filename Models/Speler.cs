using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ReversiMvcApp.Models
{
    public class Speler
    {
        [Key, Column(TypeName = "string(100)")]
        public Guid Guid { get; set; }

        public string Naam { get; set; }

        public int AantalGewonnen { get; set; }

        public int AantalVerloren { get; set; }

        public int AantalGelijk { get; set; }
    }
}
