using api_gestion_ecole.Dtos.Cours;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Interfaces
{
    public interface ICoursRepository
    {
        public Task<List<Cours>> GetAllAsync();
        public Task<Cours?> GetByIdAsync(int id);
        public Task<Cours?> CreateAsync(CreateCoursDto createCoursDto);
        public Task<Cours?> UpdateAsync(int id, UpdateCoursDto updteCoursDto);
        public Task<Cours?> DeleteAsync(int id);
    }
}