using api_gestion_ecole.Dtos.CoursConcernerClasse;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Mappers
{
    public static class CoursConcernerClasseMapper
    {
        public static CoursConcernerClasseDto ToCoursConcernerClasseDto(this CoursConcernerClasse coursConcernerClasse)
        {
            return new CoursConcernerClasseDto
            {
                Id = coursConcernerClasse.Id,
                CoursId = coursConcernerClasse.CoursId,
                ClassId = coursConcernerClasse.ClasseId,
                EnseignantId = coursConcernerClasse.EnseignantId,
                AnneeScolaireId = coursConcernerClasse.AnneeScolaireId,
                Max = coursConcernerClasse.Max,
                NombreHeures = coursConcernerClasse.NombreHeures,
                CoursNom = coursConcernerClasse?.Cours?.Designation,
                ClasseNom = coursConcernerClasse?.Classe?.Designation,
                Option = coursConcernerClasse?.Classe?.Option?.Designation,
                Enseignant = $"{coursConcernerClasse?.Enseignant?.Prenom} {coursConcernerClasse?.Enseignant?.Nom}",
                AnneeScolaire = coursConcernerClasse?.AnneeScolaire?.Designation
            };
        }
        public static CoursConcernerClasse ToCoursConcernerFromCreate(this CreateCoursConcernerClasseDto createCoursConcernerClasseDto)
        {
            return new CoursConcernerClasse
            {
                CoursId = createCoursConcernerClasseDto.CoursId,
                ClasseId = createCoursConcernerClasseDto.ClasseId,
                EnseignantId = createCoursConcernerClasseDto.EnseignantId,
                AnneeScolaireId = createCoursConcernerClasseDto.AnneeScolaireId,
                Max = createCoursConcernerClasseDto.Max,
                NombreHeures = createCoursConcernerClasseDto.NombreHeures
            };
        }
    }
}