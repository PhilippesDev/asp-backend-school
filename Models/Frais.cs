namespace api_gestion_ecole.Models
{
    public class Frais
    {
        public int Id { get; set; }
        public string Designation { get; set; } = string.Empty;
        public int CategorieFraisId { get; set; }
        public CategorieFrais? CategorieFrais { get; set; }
        public IEnumerable<FraisConcernerClasse>? FraisConcernerClasses { get; set; }

    }
}