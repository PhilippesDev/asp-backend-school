using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace api_gestion_ecole.Dtos.Auth
{
    public class UpdateUserDto 
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Le nom de l'utilisateur es requis")]
        [MinLength(5, ErrorMessage ="Le nom de l'utilisateur doit contenir au moins 5 caractères")]
        [MaxLength(20, ErrorMessage ="Le nom de l'utilisateur doit contenir au plus 20 caractères")]
        public string UserName { get; set; } = string.Empty;
        [Required(AllowEmptyStrings =false, ErrorMessage = "L'adresse mail est requis")]
        [EmailAddress(ErrorMessage ="Veillez entrer une adresse mail valide")]
        public string Email { get; set; } = string.Empty;
    }
}