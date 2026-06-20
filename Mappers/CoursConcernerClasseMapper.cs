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
                AnneeScolaireId = coursConcernerClasse.AnneeScolaireId,
                Max = coursConcernerClasse.Max,
                CoursNom = coursConcernerClasse?.Cours?.Designation,
                ClasseNom = coursConcernerClasse?.Classe?.Designation,
                Option = coursConcernerClasse?.Classe?.Option?.Designation,
                AnneeScolaire = coursConcernerClasse?.AnneeScolaire?.Designation
            };
        }
        public static CoursConcernerClasse ToCoursConcernerFromCreate(this CreateCoursConcernerClasseDto createCoursConcernerClasseDto)
        {
            return new CoursConcernerClasse
            {
                CoursId = createCoursConcernerClasseDto.CoursId,
                ClasseId = createCoursConcernerClasseDto.ClasseId,
                AnneeScolaireId = createCoursConcernerClasseDto.AnneeScolaireId,
                Max = createCoursConcernerClasseDto.Max
            };
        }
    }
}