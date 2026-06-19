using api_gestion_ecole.Dtos.CoursConcernerClasse;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Interfaces
{
    public interface ICoursConcernerClasseRepository
    {
        public Task<List<CoursConcernerClasse>> GetAllAsync();
        public Task<CoursConcernerClasse?> GetByIdAsync(int CoursId, int ClasseId, int AnneeScolaireId);
        public Task<CoursConcernerClasse?> CreateAsync(CreateCoursConcernerClasseDto  createCoursConcernerClasseDto);
        public Task<CoursConcernerClasse?> UpdateAsync(int CoursId, int ClasseId, int AnneeScolaireId, UpdateCoursConcernerClasseDto updateCoursConcernerClasseDto);
        public Task<CoursConcernerClasse?> DeleteAsync(int CoursId, int ClasseId, int AnneeScolaireId);
        public Task<bool> IsCoursExistAsync(int id);
        public Task<bool> IsClasseExistAsync(int id);
        public Task<bool> IsAnneeScolaireExistAsync(int id);
    }
}