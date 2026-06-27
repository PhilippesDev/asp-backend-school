namespace api_gestion_ecole.Dtos.Classe
{
    public class ClasseDto
    {
        public int Id { get; set; }
        public int OptionId { get; set; }
        public int NiveauId { get; set; }
        public string Designation { get; set; } = string.Empty;
        public string? Niveau { get; set; } = string.Empty;
        public string? Option { get; set; }
    }
}