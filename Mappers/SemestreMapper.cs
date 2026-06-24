using api_gestion_ecole.Dtos.Semestre;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Mappers
{
    public static class SemestreMapper
    {
        public static Semestre ToSemestreFromCreate(this CreateSemestreDto createSemestreDto)
        {
            return new Semestre
            {
                Designation = createSemestreDto.Designation,
            };
        }
        public static SemestreDto ToSemestreDto(this Semestre semestre)
        {
            return new SemestreDto
            {
                Id = semestre.Id,
                Designation = semestre.Designation,
            };
        }
    }
}