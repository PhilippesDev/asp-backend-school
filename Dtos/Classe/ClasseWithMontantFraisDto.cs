namespace api_gestion_ecole.Dtos.Classe
{
    public class ClasseWithMontantFraisDto
    {
        public int Id { get; set; }
        public int OptionId { get; set; }
        public string Designation { get; set; } = "";
        public string? Option { get; set; }
        public decimal MontantTotal { get; set; }
    }
}
