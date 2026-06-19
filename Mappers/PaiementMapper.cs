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
                Montant = paiement!.Montant
            };
        }

        public static Paiement ToPaiementFromCreate(this CreatePaiementDto createPaiementDto)
        {
            return new Paiement
            {
                InscriptionId = createPaiementDto.InsciptionId,
                FraisConcernerClasseId = createPaiementDto.FraisConcernerClasseId,
                Montant = createPaiementDto.Montant
            };
        }
    }
}