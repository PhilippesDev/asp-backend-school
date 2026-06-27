using System.ComponentModel.DataAnnotations;

namespace api_gestion_ecole.Dtos.Niveau
{
    public class CreateNiveauDto
    {
        [Required(AllowEmptyStrings =false, ErrorMessage ="Veillez entrer le nom du niveau")]
        public string Designation { get; set; } = string.Empty;
    }
}