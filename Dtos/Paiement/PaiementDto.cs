namespace api_gestion_ecole.Dtos.Paiement
{
    public class PaiementDto
    {
        public int Id { get; set; }
        public int InsciptionId { get; set; }
        public int FraisConcernerClasseId { get; set; }
        public string? Eleve {get; set;}
        public string? Classe { get; set; }
        public string? Option { get; set; }
        public string? Frais { get; set; }
        public Decimal Montant {get; set;}
        public DateTime DatePaiement {get; set; }
    }
}