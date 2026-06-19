namespace api_gestion_ecole.Models
{
    public class Cours
    {
        public int Id { get; set; }
        public string Designation { get; set; } = string.Empty;
        public string? Abreviation { get; set; }
        public IEnumerable<CoursConcernerClasse>? CoursConcernerClasses { get; set; }
        public IEnumerable<Cotation>? Cotations { get; set; }
    }
}