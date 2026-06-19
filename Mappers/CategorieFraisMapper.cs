using api_gestion_ecole.Dtos.CategorieFrais;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Mappers
{
    public static class CategorieFraisMapper
    {
        public static CategorieFrais ToCategorieFraisFromCreate(this CreateCategorieFraisDto createCategorieFraisDto)
        {
            return new CategorieFrais {Designation = createCategorieFraisDto.Designation};
        }
        public static CategorieFraisDto ToCategorieFraisDto(this CategorieFrais categorieFrais)
        {
            return new CategorieFraisDto
            {
                Id = categorieFrais.Id,
                Designation = categorieFrais.Designation,
            };
        }
    }
}