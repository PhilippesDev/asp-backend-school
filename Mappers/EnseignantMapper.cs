using api_gestion_ecole.Dtos.Enseignant;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Mappers
{
    public static  class EnseignantMapper
    {
        public static Enseignant ToEnsiegnantFromCreate(this CreateEnseignantDto createEnseignantDto)
        {
            return new Enseignant
            {
                Nom = createEnseignantDto.Nom,
                Postnom = createEnseignantDto.Postnom,
                Prenom = createEnseignantDto.Prenom,
                Sexe = createEnseignantDto.Sexe,
                DateNaissance = createEnseignantDto.DateNaissance,
                LieuNaissance = createEnseignantDto.LieuNaissance,
                NiveauEtude = createEnseignantDto.NiveauEtude,
                Specialite = createEnseignantDto.Specialite,
                Telephone = createEnseignantDto.Telephone,
                Email = createEnseignantDto.Email,
                Adresse = createEnseignantDto.Adresse,
                Photo = createEnseignantDto.Photo,
            };
        }

        public static void UpdateEnseignant(this Enseignant enseignant, UpdateEnseignantDto updateEnseignantDto)
        {
            enseignant.Nom = updateEnseignantDto.Nom;
            enseignant.Postnom = updateEnseignantDto.Postnom;
            enseignant.Prenom = updateEnseignantDto.Prenom;
            enseignant.Sexe = updateEnseignantDto.Sexe;
            enseignant.DateNaissance = updateEnseignantDto.DateNaissance;
            enseignant.LieuNaissance = updateEnseignantDto.LieuNaissance;
            enseignant.Telephone = updateEnseignantDto.Telephone;
            enseignant.Email = updateEnseignantDto.Email;
            enseignant.Adresse = updateEnseignantDto.Adresse;
            enseignant.Photo = updateEnseignantDto.Photo;
        }
        public static EnseignantDto ToEnseignantDto(this Enseignant enseignant)
        {
            return new EnseignantDto
            {
                Id = enseignant.Id,
                Nom = enseignant.Nom,
                Postnom = enseignant.Postnom,
                Prenom = enseignant.Prenom,
                Sexe = enseignant.Sexe,
                DateNaissance = enseignant.DateNaissance,
                LieuNaissance = enseignant.LieuNaissance,
                NiveauEtude = enseignant.NiveauEtude,
                Specialite = enseignant.Specialite,
                Telephone = enseignant.Telephone ,
                Email = enseignant.Email,
                Adresse = enseignant.Adresse ?? string.Empty,
                Photo = enseignant.Photo
            };
        }
    }
}