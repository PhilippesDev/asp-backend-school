using System.ComponentModel.DataAnnotations;

namespace api_gestion_ecole.Dtos
{
    public class CreateAnneeScolaireDto
    {
       [Required(AllowEmptyStrings = false, ErrorMessage = "Veillez entrez l'annnée scolaire !")]
       [MinLength(9, ErrorMessage = "L'année scolaire doit avoir au moins 9 caractères !")]
        public string Designation { get; set; } = string.Empty; 
        public DateOnly DateDebut {get; set;}
        public DateOnly DateFin {get; set;}
    }
}