using System.ComponentModel.DataAnnotations;

namespace api_gestion_ecole.Dtos.Paiement
{
    public class CreatePaiementDto
    {
        public int InscriptionId { get; set; }
        public int FraisConcernerClasseId { get; set; }
        
        [Range(0.1, 200,ConvertValueInInvariantCulture =false,
        ErrorMessage = "Le montant à payer doit être compris entre 0.1 et 200")]
        public decimal Montant { get; set; }
        public DateTime DatePaiement {get; set; } = DateTime.UtcNow;
    }
}