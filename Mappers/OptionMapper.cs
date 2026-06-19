using api_gestion_ecole.Dtos.Option_;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Mappers
{
    public static class OptionMapper
    {
        public static Option ToOptionFromCreate(this CreateOptionDto createOptionDto)
        {
            return new Option{
                Designation = createOptionDto.Designation,
                Abreviation = createOptionDto.Abreviation
            };
        }
        public static OptionDto ToOptionDto (this Option option)
        {
            return new OptionDto
            {
                Id = option.Id,
                Designation = option.Designation, 
                Abreviation = option.Abreviation
            };
        }
    }
}