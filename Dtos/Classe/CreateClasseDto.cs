using System.ComponentModel.DataAnnotations;

namespace api_gestion_ecole.Dtos.Classe
{
    public class CreateClasseDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veillez entrez le nom de la classe")]
        public string Designation { get; set; } = "";
        public int OptionId { get; set; }
        public int NiveauId { get; set; }
    }
}