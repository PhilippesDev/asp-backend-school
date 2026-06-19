using System.ComponentModel.DataAnnotations;

namespace api_gestion_ecole.Dtos.Insciption
{
    public class UpdateInscriptionDto
    {
        public int ClasseId { get; set; }
        public int AnneeScolaireId { get; set; }
    }
}