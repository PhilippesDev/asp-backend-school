using System.ComponentModel.DataAnnotations;

namespace api_gestion_ecole.Dtos.AnneeScolaire
{
    public class AnneeScolaireDto
    {
        public int Id { get; set; }
        public string Designation { get; set; } = string.Empty;
        public string? Couleur { get; set; }
        public DateOnly DateDebut {get; set;}
        public DateOnly DateFin {get; set;}
        public bool EstActive { get; set; }
    }
}