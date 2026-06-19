using api_gestion_ecole.Dtos.CategorieFrais;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Interfaces
{
    public interface ICategorieFraisRepository
    {
        public Task<List<CategorieFrais>> GetAllAsync();
        public Task<CategorieFrais?> GetByIdAsync(int id);
        public Task<CategorieFrais?> CreateAsync(CreateCategorieFraisDto createCategorieFraisDto);
        public Task<CategorieFrais?> UpdateAsync(int id, UpdateCategorieFraisDto updateCategorieFraisDto);
        public Task<CategorieFrais?> DeleteAsync(int id);
    }
}