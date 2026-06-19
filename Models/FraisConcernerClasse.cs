using System.ComponentModel.DataAnnotations.Schema;

namespace api_gestion_ecole.Models
{
    [Table("FraisConcernerClasse")]
    public class FraisConcernerClasse
    {
        public int Id { get; set; }
        public decimal Montant { get; set; }
        public int FraisId { get; set; }
        public Frais? Frais { get; set; }
        public int ClasseId { get; set; }
        public Classe? Classe { get; set; }
        public int AnneeScolaireId { get; set; }
        public AnneeScolaire? AnneeScolaire { get; set; }
        public IEnumerable<Paiement>? Paiements { get; set; }
    }
}