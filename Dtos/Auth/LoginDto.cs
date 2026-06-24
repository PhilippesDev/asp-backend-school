namespace api_gestion_ecole.Dtos.Auth
{
    public class LoginDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Email {get ; set;} = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}