using System.ComponentModel.DataAnnotations;

namespace api_gestion_ecole.Dtos.Cotation
{
    public class UpdateCotationDto
    {  
        [Range(0, 500, ConvertValueInInvariantCulture = false, 
                ErrorMessage = "Entrez une cote valide")]
        public double Cote { get; set; }
    }
}