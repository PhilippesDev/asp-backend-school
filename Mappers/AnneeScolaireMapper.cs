using api_gestion_ecole.Dtos;
using api_gestion_ecole.Dtos.AnneeScolaire;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Mappers
{
    public static class AnneeScolaireMapper
    {
        public static AnneeScolaire ToAnneeScolaireFromCreate(this CreateAnneeScolaireDto createAnneeScolaireDto)
        {
            return new AnneeScolaire
            {   Designation = createAnneeScolaireDto.Designation,
                Couleur = createAnneeScolaireDto.Couleur,
                DateDebut = createAnneeScolaireDto.DateDebut,
                DateFin = createAnneeScolaireDto.DateFin
            };
        }

        public static AnneeScolaireDto ToAnneeScolaireDto(this AnneeScolaire anneeScolaire)
        {
            return new AnneeScolaireDto
            {
                Id = anneeScolaire.Id,
                Designation = anneeScolaire.Designation,
                Couleur = anneeScolaire.Couleur,
                DateDebut = anneeScolaire.DateDebut,
                DateFin = anneeScolaire.DateFin,
                EstActive = anneeScolaire.EstActive
            };
        }
    }
}