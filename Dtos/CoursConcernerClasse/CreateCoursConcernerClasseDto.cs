namespace api_gestion_ecole.Dtos.CoursConcernerClasse
{
    public class CreateCoursConcernerClasseDto
    {
        public int CoursId { get; set; }
        public int ClasseId { get; set; }
        public int AnneeScolaireId { get; set; }
        public int Max { get; set; }
    }
}
