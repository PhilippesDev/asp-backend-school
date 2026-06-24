namespace api_gestion_ecole.Models
{
    public class Presence
    {
        public int Id { get; set; }
        public int InscriptionId { get; set; }
        public Inscription? Inscription { get; set; }
        public DateOnly DatePresence {get; set;}
    }
}