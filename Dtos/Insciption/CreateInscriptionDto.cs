using System.ComponentModel.DataAnnotations;

namespace api_gestion_ecole.Dtos.Insciption
{
    public class CreateInscriptionDto
    {
        public int EleveId {get; set;}
        public int ClasseId { get; set; }
        public int AnneeScolaireId { get; set; }
        public DateTime DateInscription { get; set; } = DateTime.UtcNow;
    }
}