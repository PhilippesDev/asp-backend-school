namespace api_gestion_ecole.Models
{
    public class Periode
    {
        public int Id { get; set; }
        public string Designation { get; set; } = string.Empty;
        public IEnumerable<Cotation>? Cotations { get; set; }
    }
}