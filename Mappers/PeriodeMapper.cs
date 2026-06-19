using api_gestion_ecole.Dtos.Periode;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Mappers
{
    public static class PeriodeMapper
    {
        public static Periode ToPeriodeFromCreate(this CreatePeriodeDto createPeriodeDto)
        {
            return new Periode
            {
                Designation = createPeriodeDto.Designation
            };
        }
        public static PeriodeDto ToPeriodeDto(this Periode periode)
        {
            return new PeriodeDto
            {
                Id = periode.Id,
                Designation = periode.Designation  
            };
        }
    }
}