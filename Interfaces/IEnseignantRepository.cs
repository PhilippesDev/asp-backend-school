using api_gestion_ecole.Dtos.Enseignant;
using api_gestion_ecole.Helpers;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Interfaces
{
    public interface IEnseignantRepository
    {
        public Task<List<Enseignant>> GetAllAsync(QueryObjectForPeople queryObject);
        public Task<Enseignant?> GetByIdAsync(int id);
        public Task<Enseignant?> CreateAsync(CreateEnseignantDto createEnseignantDto);
        public Task<Enseignant?> UpdateAsync(int id, UpdateEnseignantDto updateEnseignantDto);
        public Task<Enseignant?> DeleteAsync(int id); 
        public Task<int> GetNombreEnseignatsAsync();
    }
}