using System.ComponentModel.DataAnnotations;
namespace api_gestion_ecole.Dtos.Parent
{
    public class CreateParentDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veillez entrer le nom du parent de l'élève")]
        public string Nom { get; set; } = "";
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veillez entrer le postnom du parent de l'élève")]
        public string Postnom { get; set; } = "";
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veillez entrer le prenom du parent de l'élève")]
        public string Prenom { get; set; } = "";
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veillez entrer le genre du parent de l'élève (M ou F)")]
        [MaxLength(1, ErrorMessage = "Le genredu parent de l'élève doit avoir au plus 1 caractère. M ou F")]
        public string Sexe { get; set; } = "";
        public string Profession { get; set; } = string.Empty;
        public string Telephone{get;set;} = string.Empty;
    }
}