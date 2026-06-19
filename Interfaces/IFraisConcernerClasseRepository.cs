using api_gestion_ecole.Dtos.FraisConcernerClasse;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Interfaces
{
    public interface IFraisConcernerClasseRepository
    {
        public Task<List<FraisConcernerClasse>> GetAllAsync();
        public Task<FraisConcernerClasse?> GetByIdAsync(int id);
        public Task<FraisConcernerClasse?> CreateAsync(CreateFraisConcernerClasseDto  createFraisConcernerClasseDto);
        public Task<FraisConcernerClasse?> UpdateAsync(int id,UpdateFraisConcernerClasseDto updateFraisConcernerClasseDto);
        public Task<FraisConcernerClasse?> DeleteAsync(int id);
        public Task<bool> IsFraisExistAsync(int id);
        public Task<bool> IsClasseExistAsync(int id);
        public Task<bool> IsAnneeScolaireExistAsync(int id);
    }
}