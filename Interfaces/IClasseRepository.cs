using api_gestion_ecole.Dtos.Classe;
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
    }
}