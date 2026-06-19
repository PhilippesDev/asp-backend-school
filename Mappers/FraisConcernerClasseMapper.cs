using api_gestion_ecole.Dtos.FraisConcernerClasse;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Mappers
{
    public static class FraisConcernerClasseMapper
    {
        public static FraisConcernerClasseDto ToFraisConcernerClasseDto(this FraisConcernerClasse fraisConcernerClasse)
        {
            return new FraisConcernerClasseDto
            {
                FraisId = fraisConcernerClasse.FraisId,
                ClasseId = fraisConcernerClasse.ClasseId,
                AnneeScolaireId = fraisConcernerClasse.AnneeScolaireId,
                Montant = fraisConcernerClasse.Montant,
                Frais = fraisConcernerClasse?.Frais?.Designation,
                Classe = fraisConcernerClasse?.Classe?.Designation,
                Option = fraisConcernerClasse?.Classe?.Option?.Designation,
                AnneeScolaire = fraisConcernerClasse?.AnneeScolaire?.Designation
            };
        }
        public static FraisConcernerClasse ToFraisConcernerFromCreate(this CreateFraisConcernerClasseDto createFraisConcernerClasseDto)
        {
            return new FraisConcernerClasse
            {
                FraisId = createFraisConcernerClasseDto.FraisId,
                ClasseId = createFraisConcernerClasseDto.ClasseId,
                AnneeScolaireId = createFraisConcernerClasseDto.AnneeScolaireId,
                Montant = createFraisConcernerClasseDto.Montant
            };
        }
    }
}