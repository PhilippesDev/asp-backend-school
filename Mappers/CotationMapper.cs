using api_gestion_ecole.Dtos.Cotation;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Mappers
{
    public static class CotationMapper
    {
        public static CotationDto ToCotationDto(this Cotation cotation)
        {
            return new CotationDto
            {
                Id = cotation.Id,
                InscriptionId = cotation.InscriptionId,
                CoursConcernerClasseId = cotation.CoursConcernerClasseId,
                PeriodeId = cotation.PeriodeId,
                Eleve = $"{cotation?.Inscription?.Eleve?.Nom} " + 
                               $"{cotation?.Inscription?.Eleve?.Prenom} " +
                               $"{cotation?.Inscription?.Eleve?.Postnom}",
                Classe = cotation?.CoursConcernerClasse?.Classe?.Designation,
                Option = cotation?.CoursConcernerClasse?.Classe?.Option?.Designation,
                Periode = cotation?.Periode?.Designation,
                Cours = cotation?.CoursConcernerClasse?.Cours?.Designation,
                Cote = cotation!.Cote,
                Max = cotation.CoursConcernerClasse.Max,
                DateCotation = cotation.DateCotation
            };
            
        }
        public static Cotation ToCotationFromCreate(this CreateCotationDto createCotationDto)
        {
            return new Cotation
            {
                InscriptionId = createCotationDto.InscriptionId,
                CoursConcernerClasseId = createCotationDto.CoursConcernerClasseId,
                PeriodeId = createCotationDto.PeriodeId,
                Cote = createCotationDto.Cote,
                DateCotation = createCotationDto.DateCotation
            };
        }
    }
}