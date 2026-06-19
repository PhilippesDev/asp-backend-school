using System.ComponentModel.DataAnnotations;

namespace api_gestion_ecole.Dtos.Periode
{
    public class CreatePeriodeDto
    {
        [Required(AllowEmptyStrings =false, ErrorMessage ="Veillez entrer le nom de la periode")]
        [MinLength(5, ErrorMessage = "Le nom de la periode doit contenir au moins 5 caractères")]
        public string Designation { get; set; } = "";
    }
}