namespace api_gestion_ecole.Models
{
    public class Cotation
    {
        public double Cote { get; set; }
        public DateTime DateCotation { get; set; } = DateTime.UtcNow;
        public int InscriptionId { get; set; }
        public Inscription? Inscription { get; set; }
        public int CoursId { get; set; }
        public Cours? Cours { get; set; }
        public int PeriodeId { get; set; }
        public Periode? Periode { get; set; }
    }
}