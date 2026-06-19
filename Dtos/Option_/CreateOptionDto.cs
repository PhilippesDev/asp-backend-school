using System.ComponentModel.DataAnnotations;

namespace api_gestion_ecole.Dtos.Option_
{
    public class CreateOptionDto
    {
        [Required(AllowEmptyStrings =false, ErrorMessage ="Veillez entrer le nom de l'option")]
        public string Designation { get; set; } = string.Empty;
        public string? Abreviation { get; set; }
    }
}