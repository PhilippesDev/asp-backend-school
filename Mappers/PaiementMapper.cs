using api_gestion_ecole.Dtos.Paiement;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Mappers
{
    public static class PaiementMapper
    {
        public static PaiementDto ToPaiementDto(this Paiement paiement)
        {
            return new PaiementDto
            {
                Id = paiement.Id,
                InsciptionId = paiement.InscriptionId,
                FraisConcernerClasseId = paiement.FraisConcernerClasseId,
                Eleve = $"{paiement?.Inscription!.Eleve?.Nom} {paiement?.Inscription!.Eleve?.Postnom} {paiement?.Inscription!.Eleve?.Prenom}",
                Frais = paiement?.FraisConcernerClasse!.Frais?.Designation,
                Montant = paiement!.Montant,
                Classe = paiement?.FraisConcernerClasse?.Classe?.Designation,
                Option = paiement?.FraisConcernerClasse?.Classe?.Option?.Designation,
                DatePaiement = paiement!.DatePaiement
            };
        }

        public static Paiement ToPaiementFromCreate(this CreatePaiementDto createPaiementDto)
        {
            return new Paiement
            {
                InscriptionId = createPaiementDto.InscriptionId,
                FraisConcernerClasseId = createPaiementDto.FraisConcernerClasseId,
                Montant = createPaiementDto.Montant,
                DatePaiement = createPaiementDto.DatePaiement
            };
        }
    }
}