using System.Runtime.InteropServices;
using api_gestion_ecole.Dtos.CategorieFrais;
using api_gestion_ecole.Dtos.Classe;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Mappers
{
    public static class ClasseMapper
    {
        public static Classe ToClasseFromCreate(this CreateClasseDto createClasseDto)
        {
            return new Classe {
                Designation = createClasseDto.Designation, 
                OptionId = createClasseDto.OptionId, 
                NiveauId = createClasseDto.NiveauId
            };
        }

        public static ClasseDto ToClasseDto(this Classe classe)
        {
            return new ClasseDto
            {
                Id = classe.Id,
                OptionId = classe.OptionId,
                NiveauId = classe.NiveauId,
                Designation = classe.Designation,
                Niveau = classe?.Niveau?.Designation,
                Option = classe?.Option?.Designation
            };
        }
    }
}