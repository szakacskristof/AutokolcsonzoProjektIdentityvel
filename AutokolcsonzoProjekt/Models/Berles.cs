namespace AutokolcsonzoProjekt.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Berles
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Autó")]
        public int AutoID { get; set; }

        [Required]
        [Display(Name = "Dátum")]
        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }

        [Required]
        [Display(Name = "Kezdés ideje")]
        public TimeSpan Tol { get; set; }

        [Required]
        [Display(Name = "Befejezés ideje")]
        public TimeSpan Ig { get; set; }

        [Required]
        [Display(Name = "Megtett távolság (km)")]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Tav { get; set; }

        
        public virtual Auto? Auto { get; set; }
    }

}
