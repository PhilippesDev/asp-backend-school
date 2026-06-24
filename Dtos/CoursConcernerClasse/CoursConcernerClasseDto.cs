namespace api_gestion_ecole.Dtos.CoursConcernerClasse
{
    public class CoursConcernerClasseDto
    {
        public int Id;
        public int CoursId { get; set; }
        public int ClassId { get; set; }
        public int EnseignantId {get; set; }
        public int AnneeScolaireId { get; set; }
        public int Max { get; set; }
        public int NombreHeures {get; set;}
        public string? CoursNom { get; set; }
        public string? ClasseNom { get; set; }
        public string? Option { get; set; }
        public string Enseignant {get; set;} = string.Empty;
        public string? AnneeScolaire { get; set; }
    }
}
