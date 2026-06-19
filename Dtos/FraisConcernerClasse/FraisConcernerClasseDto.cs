namespace api_gestion_ecole.Dtos.FraisConcernerClasse
{
    public class FraisConcernerClasseDto
    {
        public int Id { get; set; }
        public int FraisId { get; set; }
        public int ClasseId { get; set; }
        public int AnneeScolaireId { get; set; }
        public string? Frais { get; set; }
        public decimal Montant { get; set; }
        public string? Classe { get; set; }
        public string? Option { get; set; }
        public string? AnneeScolaire { get; set; } 
    }
}