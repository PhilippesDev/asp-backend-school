namespace api_gestion_ecole.Models
{
    public class Option
    {
        public int Id { get; set; }
        public string Designation { get; set; } = string.Empty;
        public string? Abreviation { get; set; }
        public IEnumerable<Classe>? Classes { get; set; }
    }
}