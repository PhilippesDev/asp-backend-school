using System.ComponentModel.DataAnnotations;

namespace api_gestion_ecole.Dtos.CategorieFrais
{
    public class CreateCategorieFraisDto
    {
        [Required(AllowEmptyStrings =false, ErrorMessage ="Veillez entrer la catégorie de frais")]
        [MaxLength(40, ErrorMessage = "La catégorie frais doit avoir au plus 60 caractères")]
        public string Designation { get; set; } = ""; 
    }
}