namespace api_gestion_ecole.Dtos.Auth
{
    public class AuthentificatedUserDto
    {
        public string UserName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Token { get; set; }  = "";
    }
}