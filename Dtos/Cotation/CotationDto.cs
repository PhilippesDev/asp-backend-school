namespace api_gestion_ecole.Dtos.Cotation
{
    public class CotationDto
    {
        public int Id { get; set; }
        public int InscriptionId { get; set; }
        public int CoursConcernerClasseId { get; set; }
        public int PeriodeId { get; set; }
        public string? Eleve { get; set; }
        public string? Classe { get; set; }
        public string? Option { get; set; }
        public string? Periode { get; set; }
        public string? Cours { get; set; }
        public double Cote { get; set; }
        public int Max { get; set; }
        public DateTime DateCotation { get; set; } = DateTime.UtcNow;
       
    }
}