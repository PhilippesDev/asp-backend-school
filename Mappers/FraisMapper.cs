using api_gestion_ecole.Dtos.Frais;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Mappers
{
    public static class FraisMapper
    {
        public static Frais ToFraisFromCreate(this CreateFraisDto createFraisDto)
        {
            return new Frais
            {
                Designation = createFraisDto.Designation,
                CategorieFraisId = createFraisDto.CategorieFraisId
            };
        }
        public static FraisDto ToFraisDto(this Frais frais)
        {
            return new FraisDto
            {
                Id = frais.Id,
                Designation = frais.Designation,
                Categorie = frais?.CategorieFrais?.Designation
            };
        }
        
    }
}