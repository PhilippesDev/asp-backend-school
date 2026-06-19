using System.ComponentModel.DataAnnotations.Schema;

namespace api_gestion_ecole.Models
{
    [Table(name:"CoursConcernerClasse")]
    public class 
    CoursConcernerClasse
    {
        public int Id { get; set; }
        public int Max { get; set; }
        public int CoursId { get; set; }
        public Cours? Cours { get; set; }
        public int ClasseId { get; set; }
        public Classe? Classe {get; set; }
        public int AnneeScolaireId { get; set; }
        public AnneeScolaire? AnneeScolaire { get; set; }
        public IEnumerable<Cotation>? Cotations { get; set; }
    }
}