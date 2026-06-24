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
                SemestreId = createPeriodeDto.SemestreId,
                Designation = createPeriodeDto.Designation,
                Coefficient = createPeriodeDto.Coefficient
            };
        }
        public static PeriodeDto ToPeriodeDto(this Periode periode)
        {
            return new PeriodeDto
            {
                Id = periode.Id,
                SemestreId = periode.SemestreId,
                Designation = periode.Designation,
                Semestre = periode.Semestre?.Designation ?? string.Empty,
                Coefficient = periode.Coefficient
            };
        }
    }
}
