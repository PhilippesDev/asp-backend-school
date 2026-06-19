namespace api_gestion_ecole.Models
{
    public class Cotation
    {
        public int Id { get; set; }
        public double Cote { get; set; }
        public DateTime DateCotation { get; set; } = DateTime.UtcNow;
        public int InscriptionId { get; set; }
        public Inscription? Inscription { get; set; }
        public int CoursConcernerClasseId { get; set; }
        public CoursConcernerClasse CoursConcernerClasse { get; set; } = null!;
        public int PeriodeId { get; set; }
        public Periode? Periode { get; set; }
    }
}