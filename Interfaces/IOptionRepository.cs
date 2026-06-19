using api_gestion_ecole.Dtos.Option_;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Interfaces
{
    public interface IOptionRepository
    {
        public Task<List<Option>> GetAllAsync();
        public Task<Option?> GetByIdAsync(int id);
        public Task<Option?> CreateAsync(CreateOptionDto createOptionDto);
        public Task<Option?> UpdateAsync(int id, UpdateOptionDto updateOptionDto);
        public Task<Option?> DeleteAsync(int id);
        
    }
}