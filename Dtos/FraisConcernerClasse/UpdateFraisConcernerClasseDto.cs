using System.ComponentModel.DataAnnotations;

namespace api_gestion_ecole.Dtos.FraisConcernerClasse
{
    public class UpdateFraisConcernerClasseDto
    {
        [Range(0.1,2000,ConvertValueInInvariantCulture =false,
        ErrorMessage = "Le montant à payer doit être compris entre 0.1 et 2000")]
        public decimal Montant { get; set; }
    }
}