namespace api_gestion_ecole.Dtos.Insciption
{
    public class InscriptionDto
    {
        public int EleveId {get; set;}
        public string? NomsEleve {get; set; }
        public string? Classe { get; set; }
        public string? Option {get; set; }
        public string? AnneeScolaire { get; set; }
        public DateTime DateInsciption { get; set; } = DateTime.UtcNow;
    }
}