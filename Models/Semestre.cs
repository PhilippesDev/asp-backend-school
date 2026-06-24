namespace api_gestion_ecole.Models
{
    public class Semestre
    {
        public int Id { get; set; }
        public string Designation { get; set; } = string.Empty; 
        public IEnumerable<Periode>? Periodes { get; set; }
    }
}