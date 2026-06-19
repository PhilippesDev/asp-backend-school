namespace api_gestion_ecole.Models
{
    public class Eleve
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Postnom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public string Sexe { get; set; } = string.Empty;
        public DateOnly? DateNaissance { get; set; }
        public string? LieuNaissance { get; set; } = string.Empty;
        public string NomsPere { get; set; } = string.Empty;
        public string NomsMere { get; set; } = string.Empty;
        public string? NumPere { get; set; }  = string.Empty;
        public string? NumMere { get; set; } = string.Empty;
        public string? Photo { get; set; } = string.Empty;
        public IEnumerable<Inscription>? Insciptions { get; set; }
    }
}