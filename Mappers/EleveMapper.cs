using api_gestion_ecole.Dtos.Classe;
using api_gestion_ecole.Dtos.Eleve;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Mappers
{
    public static class EleveMapper
    {
        public static Eleve ToEleveFromCreate(this CreateEleveDto createEleveDto)
        {
            return new Eleve
            {
                Nom = createEleveDto.Nom,
                Postnom = createEleveDto.Postnom,
                Prenom = createEleveDto.Prenom,
                Sexe = createEleveDto.Sexe,
                DateNaissance = createEleveDto.DateNaissance,
                LieuNaissance = createEleveDto.LieuNaissance,
                Adresse = createEleveDto.Adresse,
                NomsPere = createEleveDto.NomsPere,
                NomsMere = createEleveDto.NomsMere,
                NumPere = createEleveDto.NumPere,
                NumMere = createEleveDto.NumMere,
                Photo = createEleveDto.Photo,
            };
        }

        public static void UpdateEleve(this Eleve eleve, UpdateEleveDto updateEleveDto)
        {
            eleve.Nom = updateEleveDto.Nom;
            eleve.Postnom = updateEleveDto.Postnom;
            eleve.Prenom = updateEleveDto.Prenom;
            eleve.Sexe = updateEleveDto.Sexe;
            eleve.DateNaissance = updateEleveDto.DateNaissance;
            eleve.LieuNaissance = updateEleveDto.LieuNaissance;
            eleve.Adresse = updateEleveDto.Adresse;
            eleve.NomsPere = updateEleveDto.NomsPere;
            eleve.NomsMere = updateEleveDto.NomsMere;
            eleve.NumPere = updateEleveDto.NumPere;
            eleve.NumMere = updateEleveDto.NumMere;
            eleve.Photo = updateEleveDto.Photo;
        }
        public static EleveDto ToEleveDto(this Eleve eleve)
        {
            return new EleveDto
            {
                Id = eleve.Id,
                Nom = eleve.Nom,
                Postnom = eleve.Postnom,
                Prenom = eleve.Prenom,
                Sexe = eleve.Sexe,
                DateNaissance = eleve.DateNaissance,
                LieuNaissance = eleve.LieuNaissance,
                Adresse = eleve.Adresse,
                NomsPere = eleve.NomsPere,
                NomsMere = eleve.NomsMere,
                NumPere = eleve.NumPere ,
                NumMere = eleve.NumMere,
                Photo = eleve.Photo
            };
        }
    }
}