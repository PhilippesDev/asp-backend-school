using System.ComponentModel.DataAnnotations;
namespace api_gestion_ecole.Dtos.Classe
{
    public class CreateEleveDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veillez entrer le nom de l'élève")]
        public string Nom { get; set; } = "";
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veillez entrer le postnom de l'élève")]
        public string Postnom { get; set; } = "";
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veillez entrer le prenom de l'élève")]
        public string Prenom { get; set; } = "";
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veillez entrer le genre de l'élève (M ou F)")]
        [MaxLength(1, ErrorMessage = "Le genre de l'élève doit avoir au plus 1 caractère. M ou F")]
        public string Sexe { get; set; } = "";
        public DateOnly? DateNaissance { get; set; }
        public string? LieuNaissance { get; set; } = "";
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veillez entrer les noms du père de l'élève")]
        public string Adresse { get; set; } = string.Empty;
        public string NomsPere { get; set; } = "";
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veillez entrer s noms de la mère de l'élève")]
        public string NomsMere { get; set; } = "";
        public string? NumPere { get; set; } = "";
        public string? NumMere { get; set; } = "";
        public string? Photo {get; set;} = string.Empty;
        public IFormFile? ImageFile { get; set; }
    }
}