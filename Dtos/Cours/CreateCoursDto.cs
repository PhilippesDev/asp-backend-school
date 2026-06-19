using System.ComponentModel.DataAnnotations;

namespace api_gestion_ecole.Dtos.Cours
{
    public class CreateCoursDto
    {
        [Required(AllowEmptyStrings =false, ErrorMessage ="Veillez entrer le nom du cours")]
        public string Designation { get; set; } = "";
        public string? Abreviation { get; set; }
    }
}