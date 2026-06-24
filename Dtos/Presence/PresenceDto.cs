using System.ComponentModel.DataAnnotations;

namespace api_gestion_ecole.Dtos.AnneeScolaire
{
    public class PresenceDto
    {
        public int Id { get; set; }
        public int InscriptionId { get; set; }
        public string Eleve { get; set; } = string.Empty;
        public string Classe { get; set; } = string.Empty;
        public string Option { get; set; } = string.Empty;
        public DateOnly DatePresence {get; set;} = DateOnly.FromDateTime(DateTime.UtcNow);
    }
}