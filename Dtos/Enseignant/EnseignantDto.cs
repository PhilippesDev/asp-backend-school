namespace api_gestion_ecole.Dtos.Enseignant
{
    public class EnseignantDto
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Postnom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public string Sexe { get; set; } = string.Empty;
        public DateOnly? DateNaissance { get; set; }
        public string? LieuNaissance { get; set; } = string.Empty;
        public string NiveauEtude { get; set; } = string.Empty;
        public string Specialite { get; set; } = string.Empty;
        public string? Telephone { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string Adresse { get; set; } = string.Empty;
        public string? Photo { get; set; } = string.Empty;
    }
}