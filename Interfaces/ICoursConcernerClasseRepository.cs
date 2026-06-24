using api_gestion_ecole.Dtos.CoursConcernerClasse;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Interfaces
{
    public interface ICoursConcernerClasseRepository
    {
        public Task<List<CoursConcernerClasse>> GetAllAsync();
        public Task<CoursConcernerClasse?> GetByIdAsync(int id);
        public Task<CoursConcernerClasse?> CreateAsync(CreateCoursConcernerClasseDto  createCoursConcernerClasseDto);
        public Task<CoursConcernerClasse?> UpdateAsync(int id, UpdateCoursConcernerClasseDto updateCoursConcernerClasseDto);
        public Task<CoursConcernerClasse?> DeleteAsync(int id);
        public Task<bool> IsCoursExistAsync(int id);
        public Task<bool> IsClasseExistAsync(int id);
        public Task<bool> IsAnneeScolaireExistAsync(int id);
        public  Task<bool> IsEnseignantExistAsync(int id);
    }
}