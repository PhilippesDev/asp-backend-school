using api_gestion_ecole.Dtos;
using api_gestion_ecole.Dtos.AnneeScolaire;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Interfaces
{
    public interface IAnneeScolaireRepository
    {
        public Task<List<AnneeScolaire>> GetAllAsync();
        public Task<AnneeScolaire?> GetByIdAsync(int id);
        public Task<AnneeScolaire?> CreateAsync(CreateAnneeScolaireDto createAnneeScolaireDto);
        public Task<AnneeScolaire?> UpdateAsync(int id, UpdateAnneeScolaireDto updateAnneeScolaireDto);
        public Task<AnneeScolaire?> DeleteAsync(int id);
        public Task<bool> IsAnneeScolaireExist(int id,string designation);
        public Task<AnneeScolaire?> ActiveAnneeScolaireAsync(int id);
    }
}