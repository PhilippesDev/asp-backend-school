namespace api_gestion_ecole.Models
{
    public class CategorieFrais
    {
        public int Id { get; set; }
        public string Designation { get; set; } = string.Empty;
        public IEnumerable<Frais>? Frais { get; set; }
    }
}