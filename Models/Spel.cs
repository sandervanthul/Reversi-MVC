using System.ComponentModel.DataAnnotations;

namespace ReversiMvcApp.Models
{
    public enum Kleur { Geen, Wit, Zwart };

    public class Spel
    {
        public int ID { get; set; }

        [MaxLength(255)]
        public string Omschrijving { get; set; }

        [MaxLength(255)]
        public string Token { get; set; }

        [MaxLength(255)]
        public string Speler1Token { get; set; }

        [MaxLength(255)]
        public string Speler2Token { get; set; }

        public Kleur AandeBeurt { get; set; }
    }
}
