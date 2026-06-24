using System.ComponentModel.DataAnnotations;

namespace api_gestion_ecole.Dtos.Auth
{
    public class RegistorUserDto
    {
        [Required(AllowEmptyStrings =false, ErrorMessage = "Le nom de l'utilisateur es requis")]
        [MinLength(5, ErrorMessage ="Le nom de l'utilisateur doit contenir au moins 5 caractères")]
        [MaxLength(20, ErrorMessage ="Le nom de l'utilisateur doit contenir au plus 20 caractères")]
        public string UserName { get; set; } = string.Empty;
        [Required(AllowEmptyStrings =false, ErrorMessage = "L'adresse mail est requis")]
        [EmailAddress(ErrorMessage ="Veillez entrer une adresse mail valide")]
        public string Email { get; set; } = string.Empty;
        [Required(AllowEmptyStrings =false, ErrorMessage ="Le mot de passe est requis")]
        [MinLength(5, ErrorMessage ="Le mot de passe doit contenir au moins 8 caractères")]
        public string Password { get; set; } = string.Empty;
        public string Telephone_Parent { get; set; } = string.Empty;
    }
}