namespace api_gestion_ecole.Models
{
    public class AnneeScolaire
    {
        public int Id { get; set; }
        public string Designation { get; set; } = string.Empty;
        public DateOnly DateDebut {get; set;}
        public DateOnly DateFin {get; set;}
        public bool EstActive { get; set; }
        public IEnumerable<Inscription>? Insciptions { get; set; }
        public IEnumerable<CoursConcernerClasse>? CoursConcernerClasses { get; set; }
        public IEnumerable<FraisConcernerClasse>? FraisConcernerClasses { get; set; }

    }
}