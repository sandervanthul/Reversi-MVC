using System.ComponentModel.DataAnnotations;

namespace ReversiMvcApp.Models
{
    public class Speler
    {
        [Key, MaxLength(100)]
        public string Guid { get; set; }

        [MaxLength(50)]
        public string Naam { get; set; }

        public int AantalGewonnen { get; set; }

        public int AantalVerloren { get; set; }

        public int AantalGelijk { get; set; }
    }
}
