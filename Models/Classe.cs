namespace api_gestion_ecole.Models
{
    public class Classe
    {
        public int Id { get; set; }
        public string Designation { get; set; } = string.Empty;
        public int OptionId { get; set; }
        public Option Option { get; set; } = null!;
        public int NiveauId { get; set; }
        public Niveau Niveau { get; set; } = null!;
        public IEnumerable<Inscription>? Insciptions { get; set; }
        public IEnumerable<FraisConcernerClasse>? FraisConcernerClasses { get; set; }
        public IEnumerable<CoursConcernerClasse>? CoursConcernerClasses { get; set; }
    }
}