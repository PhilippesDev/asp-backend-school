using api_gestion_ecole.Dtos.Semestre;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Interfaces
{
    public interface ISemestreRepository
    {
        public Task<List<Semestre>> GetAllAsync();
        public Task<Semestre?> GetByIdAsync(int id);
        public Task<Semestre?> CreateAsync(CreateSemestreDto createSemestreDto);
        public Task<Semestre?> UpdateAsync(int id, UpdateSemestreDto updateSemestreDto);
        public Task<Semestre?> DeleteAsync(int id);
    }
}