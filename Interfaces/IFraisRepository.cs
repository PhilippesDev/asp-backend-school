using api_gestion_ecole.Dtos.Frais;
using api_gestion_ecole.Helpers;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Interfaces
{
    public interface IFraisRepository
    {
        public Task<List<Frais>> GetAllAsync(QueryObject queryObject);
        public Task<Frais?> GetByIdAsync(int id);
        public Task<Frais?> CreateAsync(CreateFraisDto  createFraisDto);
        public Task<Frais?> UpdateAsync(int id, UpdateFraisDto updateFraisDto);
        public Task<Frais?> DeleteAsync(int id);
        public Task<bool> IsCategorieFraisExistAsync(int id);
        public Task<int> GetNombreFraisAsync();
        public Task<int?> GetNombreClassesConcerneesAsync(int fraisId, string anneeScolaireDesignation);
        public Task<List<FraisConcernerClasse>?> GetListedeFraisPourClasseAsync(int classeId, string anneeScolaireDesignation);
    }
}