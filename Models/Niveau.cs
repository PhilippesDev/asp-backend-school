namespace api_gestion_ecole.Models
{
    public class Niveau
    {
        public int Id { get; set; }
        public string Designation { get; set; } = string.Empty;
        public IEnumerable<Classe>? Classes { get; set; }
    }
}