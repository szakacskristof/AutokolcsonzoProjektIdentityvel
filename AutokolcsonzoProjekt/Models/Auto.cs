using System.ComponentModel.DataAnnotations;

namespace AutokolcsonzoProjekt.Models
{
   

    public class Auto
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Rendszám")]
        public string Rendszam { get; set; } = null!;

        [Required]
        [Display(Name = "Meghajtás típusa")]
        public string Meghajtas { get; set; } = null!;

        [Display(Name = "Új jármű")]
        public bool Uj { get; set; }
    }

}
