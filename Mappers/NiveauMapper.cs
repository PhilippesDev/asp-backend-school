using api_gestion_ecole.Dtos.Niveau;
using api_gestion_ecole.Dtos.Option_;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Mappers
{
    public static class NiveauMapper
    {
        public static Niveau ToNiveauFromCreate(this CreateNiveauDto createNiveauDto)
        {
            return new Niveau{
                Designation = createNiveauDto.Designation
            };
        }
        public static NiveauDto ToNiveauDto (this Niveau niveau)
        {
            return new NiveauDto
            {
                Id = niveau.Id,
                Designation = niveau.Designation
            };
        }
    }
}