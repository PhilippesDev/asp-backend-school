using api_gestion_ecole.Dtos.Classe;
using api_gestion_ecole.Dtos.CoursConcernerClasse;
using api_gestion_ecole.Dtos.FraisConcernerClasse;
using api_gestion_ecole.Helpers;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Interfaces
{
    public interface IClasseRepository
    {
        public Task<List<Classe>> GetAllAsync();
        public Task<Classe?> GetByIdAsync(int id);
        public Task<Classe?> CreateAsync(CreateClasseDto  createClasseDto);
        public Task<Classe?> UpdateAsync(int id, UpdateClasseDto updateClasseDto);
        public Task<Classe?> DeleteAsync(int id);
        public Task<bool> IsOptionExitAsync(int id);
        public Task<bool> IsNiveauExitAsync(int id);
        public Task<int> GetNombreClasseAsync();
        public Task<int?> GetNombreEleveInClasseAsync(int classeId, string anneeScolaireDesignation);
        public Task<List<ClasseWithNombreEleves>?> GetNombreEleveParClasseAsync(string anneeScolaireDesignation,QueryObject queryObject);
        public Task<int?> GetNombreCoursInClasseAsync(int classeId, string anneeScolaireDesignation);
        public Task<decimal?> GetMontantAPayerInClasseAsync(int classeId, string anneeScolaireDesignation);
        public Task<List<ClasseWithMontantFraisDto>?> GetMontantFraisParClasseAsync(string anneeScolaireDesignation, QueryObject queryObject);
        public Task<List<CoursConcernerClasse>?> GetCoursInClasseAsync(int classeId, string anneeScolaireDesignation);
        public Task<List<FraisConcernerClasse>?> GetFraisInClasseAsync(int classeId, string anneeScolaireDesignation);
    }
}