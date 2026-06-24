using api_gestion_ecole.Dtos.Presence;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Interfaces
{
    public interface IPresenceRepository
    {
        public Task<List<Presence>> GetAllAsync();
        public Task<Presence?> GetByIdAsync(int id);
        public Task<Presence?> CreateAsync(CreatePresenceDto createAnneeScolaireDto);
        public Task<Presence?> DeleteAsync(int id);
        public Task<bool> IsInscriptionExist(int id);
        public Task<int> GetNombreELevesPresents();
        public Task<int> GetNombreELevesAbsents(int anneeScolaireId);
        public Task<int> GetNombreELevesPresentsInClasse(int classeId);
        public Task<int> GetNombreELevesAbsentsInClasse(int classeId,  int anneeScolaireActiveId);
    }
}