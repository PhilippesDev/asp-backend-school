using System.ComponentModel.DataAnnotations;

namespace api_gestion_ecole.Dtos.Periode
{
    public class CreatePeriodeDto
    {
        public int SemestreId { get; set; }
        
        [Required(AllowEmptyStrings =false, ErrorMessage ="Veillez entrer le nom de la periode")]
        [MinLength(5, ErrorMessage = "Le nom de la periode doit contenir au moins 5 caractères")]
        public string Designation { get; set; } = "";

        [Range(0.1, double.MaxValue, ErrorMessage = "Le coefficient doit être supérieur à 0")]
        public double Coefficient { get; set; } = 1.0;
    }
}