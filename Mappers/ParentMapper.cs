using api_gestion_ecole.Dtos.Classe;
using api_gestion_ecole.Dtos.Eleve;
using api_gestion_ecole.Dtos.Parent;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Mappers
{
    public static class ParentMapper
    {
        public static Parent ToParentFromCreate(this CreateParentDto createParentDto)
        {
            return new Parent
            {
                Nom = createParentDto.Nom,
                Postnom = createParentDto.Postnom,
                Prenom = createParentDto.Prenom,
                Sexe = createParentDto.Sexe,
                Profession = createParentDto.Profession,
                Telephone = createParentDto.Telephone, 
            };
        }

        public static void UpdateParent(this Parent parent, UpdateParentDto updateParentDto)
        {
            parent.Nom = updateParentDto.Nom;
            parent.Postnom = updateParentDto.Postnom;
            parent.Prenom = updateParentDto.Prenom;
            parent.Sexe = updateParentDto.Sexe;
            parent.Profession = updateParentDto.Profession;
            parent.Telephone = updateParentDto.Telephone;
        }

        public static ParentDto ToParentDto(this Parent parent)
        {
            return new ParentDto
            {
                Id = parent.Id,
                Nom = parent.Nom,
                Postnom = parent.Postnom,
                Prenom = parent.Prenom,
                Sexe = parent.Sexe,
                Profession = parent.Profession,
                Telephone = parent.Telephone
            };
        }
    }
}