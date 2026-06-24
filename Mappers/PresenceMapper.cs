using api_gestion_ecole.Dtos.AnneeScolaire;
using api_gestion_ecole.Dtos.Paiement;
using api_gestion_ecole.Dtos.Presence;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Mappers
{
    public static class PresenceMapper
    {
        public static PresenceDto ToPresenceDto(this Presence presence)
        {
            return new PresenceDto
            {
                Id = presence.Id,
                InscriptionId = presence.InscriptionId,
                Eleve = $"{presence?.Inscription!.Eleve?.Nom} {presence?.Inscription!.Eleve?.Postnom} {presence?.Inscription!.Eleve?.Prenom}",
                Classe = presence!.Inscription!.Classe!.Designation,
                Option = presence!.Inscription.Classe!.Option!.Designation,
                DatePresence = presence.DatePresence
            };
        }

        public static Presence ToPresenceFromCreate(this CreatePresenceDto createPresenceDto)
        {
            return new Presence
            {
                InscriptionId = createPresenceDto.InscriptionId,
                DatePresence =  DateOnly.FromDateTime(DateTime.UtcNow)
            };
        }
    }
}