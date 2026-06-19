using api_gestion_ecole.Dtos.FraisConcernerClasse;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Interfaces
{
    public interface IFraisConcernerClasseRepository
    {
        public Task<List<FraisConcernerClasse>> GetAllAsync();
        public Task<FraisConcernerClasse?> GetByIdAsync(int fraisId, int classeId, int anneeScolaireId);
        public Task<FraisConcernerClasse?> CreateAsync(CreateFraisConcernerClasseDto  createFraisConcernerClasseDto);
        public Task<FraisConcernerClasse?> UpdateAsync(int fraisId, int classeId, int anneeScolaireId,UpdateFraisConcernerClasseDto updateFraisConcernerClasseDto);
        public Task<FraisConcernerClasse?> DeleteAsync(int fraisId, int classeId, int nneeScolaireId);
        public Task<bool> IsFraisExistAsync(int id);
        public Task<bool> IsClasseExistAsync(int id);
        public Task<bool> IsAnneeScolaireExistAsync(int id);
    }
}