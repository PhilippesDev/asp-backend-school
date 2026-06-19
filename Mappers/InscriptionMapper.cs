using api_gestion_ecole.Dtos.Insciption;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Mappers
{
    public static class InscriptionMapper
    {
        public static InscriptionDto ToInscriptionDto(this Inscription insciption)
        {
            return new InscriptionDto
            {
              EleveId = insciption.EleveId,
              NomsEleve = $"{insciption?.Eleve?.Nom} {insciption?.Eleve?.Postnom} {insciption?.Eleve?.Prenom}",
              AnneeScolaire = insciption?.AnneeScolaire?.Designation,
              Classe = insciption?.Classe?.Designation,
              Option = insciption?.Classe?.Option?.Designation,
              DateInsciption = insciption!.DateInscription
            };
        }

        public static Inscription ToInscriptionFromCreate(this CreateInscriptionDto createInscriptionDto)
        {
            return new Inscription
            {
              EleveId = createInscriptionDto.EleveId,
              ClasseId = createInscriptionDto.ClasseId,
              AnneeScolaireId = createInscriptionDto.AnneeScolaireId,
              DateInscription = createInscriptionDto.DateInscription
            };
        }
    }
}