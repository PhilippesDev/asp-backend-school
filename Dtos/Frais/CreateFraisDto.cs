using System.ComponentModel.DataAnnotations;

namespace api_gestion_ecole.Dtos.Frais
{
    public class CreateFraisDto
    {
        [Required(AllowEmptyStrings =false, ErrorMessage ="Veillez entre le nom du frais")]
        public string Designation { get; set; } = string.Empty;
        public int CategorieFraisId { get; set; }
    }
}