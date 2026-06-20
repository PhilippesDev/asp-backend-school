using System.ComponentModel.DataAnnotations;

namespace api_gestion_ecole.Dtos.Cotation
{
    public class CreateCotationDto
    {
        public int InscriptionId { get; set; }
        public int CoursConcernerClasseId { get; set; }
        public int PeriodeId { get; set; }
        [Range(0, 500, ConvertValueInInvariantCulture = false, 
                ErrorMessage = "Entrez une cote valide")]
        public double Cote { get; set; }
        public DateTime DateCotation {get; set;} = DateTime.UtcNow;
    }
}