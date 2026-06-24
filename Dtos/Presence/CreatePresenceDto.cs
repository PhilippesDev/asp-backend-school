using System.ComponentModel.DataAnnotations;

namespace api_gestion_ecole.Dtos.Presence
{
    public class CreatePresenceDto
    {
        public int InscriptionId { get; set; }
        public DateOnly DatePresence {get; set;} = DateOnly.FromDateTime(DateTime.UtcNow);
    }
}