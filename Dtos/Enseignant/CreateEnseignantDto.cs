using System.ComponentModel.DataAnnotations;

namespace api_gestion_ecole.Dtos.Enseignant
{
    public class CreateEnseignantDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veillez entrer le nom de l'enseignant")]
        public string Nom { get; set; } = "";
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veillez entrer le postnom de l'enseignant")]
        public string Postnom { get; set; } = "";
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veillez entrer le prenom de l'enseignant")]
        public string Prenom { get; set; } = "";
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veillez entrer le genre de l'enseignant (M ou F)")]
        [MaxLength(1, ErrorMessage = "Le genre de l'enseignant doit avoir au plus 1 caractère. M ou F")]
        public string Sexe { get; set; } = "";
        public DateOnly? DateNaissance { get; set; }
        public string? LieuNaissance { get; set; } = ""; 
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veillez entrer le niveau d'étude de l'enseignant")]  
        public string NiveauEtude { get; set; } = string.Empty;
        [Required(AllowEmptyStrings = false, ErrorMessage = "Veillez entrer la spécialité de l'enseignant")]  
        public string Specialite { get; set; } = string.Empty;
        public string? Telephone { get; set; } = "";
        public string? Email { get; set; } = "";
        public string Adresse { get; set; } = string.Empty;
        public string? Photo {get; set;} = string.Empty;
        public IFormFile? ImageFile { get; set; }
    }
}