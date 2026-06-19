using api_gestion_ecole.Dtos.Cours;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Mappers
{
    public static class CoursMapper
    {
        public static Cours ToCoursFromCreate(this CreateCoursDto createCoursDto)
        {
            return new Cours
             {
                Designation = createCoursDto.Designation,
                Abreviation = createCoursDto.Abreviation
             };
        }

        public static CoursDto ToCoursDto(this Cours cours)
        {
            return new CoursDto {
                Id = cours.Id,
                Designation = cours.Designation,
                Abreviation = cours.Abreviation
            };
        }
    }
}