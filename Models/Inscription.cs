using System.ComponentModel.DataAnnotations.Schema;

namespace api_gestion_ecole.Models
{
    [Table(name:"Inscription")]
    public class Inscription
    {
        public int Id { get; set; }
        public DateTime DateInscription { get; set; } = DateTime.UtcNow;
        public int EleveId {get; set;}
        public Eleve? Eleve { get; set; }
        public int ClasseId { get; set; }
        public Classe? Classe { get; set; }
        public int AnneeScolaireId { get; set; }
        public AnneeScolaire? AnneeScolaire {get; set;}
        
        public IEnumerable<Paiement>? Paiements { get; set; }
        public IEnumerable<Cotation>? Cotations { get; set; }
    }
}