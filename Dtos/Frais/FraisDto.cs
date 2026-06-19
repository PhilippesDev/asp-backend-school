namespace api_gestion_ecole.Dtos.Frais
{
    public class FraisDto
    {
        public int Id { get; set; }
        public string Designation { get; set; } = string.Empty;
        public string? Categorie { get; set; }
    }
}