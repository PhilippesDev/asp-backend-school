namespace api_gestion_ecole.Models
{
    public class Periode
    {
        public int Id { get; set; }
        public string Designation { get; set; } = string.Empty;
        public double Coefficient { get; set; } = 1.0;
        public int SemestreId { get; set; }
        public Semestre? Semestre { get; set; }
        public IEnumerable<Cotation>? Cotations { get; set; }
    }
}