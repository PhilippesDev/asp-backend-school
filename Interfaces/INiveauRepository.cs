using api_gestion_ecole.Dtos.Niveau;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Interfaces
{
    public interface INiveauRepository
    {
        public Task<List<Niveau>> GetAllAsync();
        public Task<Niveau?> GetByIdAsync(int id);
        public Task<Niveau?> CreateAsync(CreateNiveauDto createNiveauDto);
        public Task<Niveau?> UpdateAsync(int id, UpdateNiveauDto updateNiveauDto);
        public Task<Niveau?> DeleteAsync(int id);
    }
}