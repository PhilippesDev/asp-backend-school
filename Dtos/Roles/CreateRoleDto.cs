using System.ComponentModel.DataAnnotations;

namespace api_gestion_ecole.Dtos.Roles
{
    public class CreateRoleDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Le nom du role est requis !")]
        [MinLength(4, ErrorMessage = "Le nom du rôle doit avoir au moins 4 caractères")]
        [MaxLength(15, ErrorMessage = "Le nom du rôle doit avoir au plus 15 caractères")]
        public string Name { get; set; } = string.Empty;
    }
}