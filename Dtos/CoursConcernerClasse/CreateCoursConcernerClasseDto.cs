using System.ComponentModel.DataAnnotations;

namespace api_gestion_ecole.Dtos.CoursConcernerClasse
{
    public class CreateCoursConcernerClasseDto
    {
        public int CoursId { get; set; }
        public int ClasseId { get; set; }
        public int EnseignantId { get; set; }
        public int AnneeScolaireId { get; set; }
        [Range(5,150, ErrorMessage = "Le maximum d'un cours doit être supérieur à 5 et inférieur à 150")]
        public int Max { get; set; }
        [Range(1,10, ErrorMessage = "Le nombre d'heures d'un cours doit être compris entre 1 et 10 heures")]
        public int NombreHeures {get; set;}
    }
}
