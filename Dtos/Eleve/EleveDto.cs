using Microsoft.VisualBasic;

namespace api_gestion_ecole.Dtos.Eleve
{
    public class EleveDto
    {
        public int Id { get; set; }
        public string Nom { get; set; } = "";
        public string Postnom { get; set; } = "";
        public string Prenom { get; set; } = "";
        public string Sexe { get; set; } = "";
        public DateOnly? DateNaissance { get; set; }
        public string? LieuNaissance { get; set; } = "";
        public string Adresse { get; set; } = string.Empty;
        public string NomsPere { get; set; } = "";
        public string NomsMere { get; set; } = "";
        public string? NumPere { get; set; } = "";
        public string? NumMere { get; set; } = "";
        public string? Photo { get; set; }
    }
}