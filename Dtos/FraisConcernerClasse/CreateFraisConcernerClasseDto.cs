using System.ComponentModel.DataAnnotations;

namespace api_gestion_ecole.Dtos.FraisConcernerClasse
{
    public class CreateFraisConcernerClasseDto
    {
        public int FraisId { get; set; }
        public int ClasseId { get; set; }
        public int AnneeScolaireId { get; set; }
        
        [Range(0.1,2000,ConvertValueInInvariantCulture =false,
        ErrorMessage = "Le montant à payer doit être compris entre 0.1 et 2000")]
        public decimal Montant { get; set; }
    }
}