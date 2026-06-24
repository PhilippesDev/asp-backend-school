using api_gestion_ecole.Dtos.Classe;
using api_gestion_ecole.Dtos.Eleve;
using api_gestion_ecole.Helpers;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Interfaces
{
    public interface IEleveRepository
    {
        public Task<List<Eleve>> GetAllAsync();
        public Task<Eleve?> GetByIdAsync(int id);
        public Task<Eleve?> CreateAsync(CreateEleveDto createEleveDto);
        public Task<Eleve?> UpdateAsync(int id, UpdateEleveDto updateEleveDto);
        public Task<Eleve?> DeleteAsync(int id);
        public Task<int> GetNombreElevesAsync();
        public Task<int?> GetNombreInscriptionsAsync(int eleveId);
    }
}