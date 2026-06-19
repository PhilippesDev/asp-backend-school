using System.ComponentModel.DataAnnotations.Schema;

namespace api_gestion_ecole.Models
{
    [Table("Paiement")]
    public class Paiement
    {
        public int Id { get; set; }
        public int InscriptionId { get; set; }
        public Inscription? Inscription { get; set; }
        public int FraisConcernerClasseId { get; set; }
        public FraisConcernerClasse? FraisConcernerClasse { get; set; }
        public Decimal Montant {get; set;}
        public DateTime DatePaiement {get; set; } = DateTime.UtcNow;
    }
}