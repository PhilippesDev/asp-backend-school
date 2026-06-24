using api_gestion_ecole.Dtos.Cours;
using api_gestion_ecole.Helpers;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Interfaces
{
    public interface ICoursRepository
    {
        public Task<List<Cours>> GetAllAsync(QueryObject queryObject);
        public Task<Cours?> GetByIdAsync(int id);
        public Task<Cours?> CreateAsync(CreateCoursDto createCoursDto);
        public Task<Cours?> UpdateAsync(int id, UpdateCoursDto updteCoursDto);
        public Task<Cours?> DeleteAsync(int id);
        public Task<int> GetNombreCoursAsync();
        public Task<int?> GetNombreClassesConcerneesAsync(int coursId, string anneeScolaireDesignation);
        public Task<List<CoursConcernerClasse>?> GetListedeCoursPourClasseAsync(int classeId, string anneeScolaireDesignation);
    }
}