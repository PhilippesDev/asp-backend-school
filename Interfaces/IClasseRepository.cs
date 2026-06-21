using api_gestion_ecole.Dtos.Classe;
using api_gestion_ecole.Helpers;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Interfaces
{
    public interface IClasseRepository
    {
        public Task<List<Classe>> GetAllAsync(QueryObject queryObject);
        public Task<Classe?> GetByIdAsync(int id);
        public Task<Classe?> CreateAsync(CreateClasseDto  createClasseDto);
        public Task<Classe?> UpdateAsync(int id, UpdateClasseDto updateClasseDto);
        public Task<Classe?> DeleteAsync(int id);
        public Task<bool> IsOptionExitAsync(int id);
        public Task<int> GetNombreClasseAsync();
        public Task<int?> GetNombreEleveInClasseAsync(int classeId, string anneeScolaireDesignation);
        public Task<List<ClasseWithNombreEleves>?> GetNombreEleveParClasseAsync(string anneeScolaireDesignation,QueryObject queryObject);
        public Task<int?> GetNombreCoursInClasseAsync(int classeId, string anneeScolaireDesignation);
        public Task<decimal?> GetMontantAPayerInClasseAsync(int classeId, string anneeScolaireDesignation);
        public Task<Classe?> GetMontantAPayerParClasseAsync(int classeId, string anneeScolaireDesignation);
    }
}