using api_gestion_ecole.Dtos.Eleve;
using api_gestion_ecole.Dtos.Parent;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Interfaces
{
    public interface IParentRepository
    {
        public Task<List<Parent>> GetAllAsync();
        public Task<Parent?> GetByIdAsync(int id);
        public Task<Parent?> CreateAsync(CreateParentDto createParentDto);
        public Task<Parent?> UpdateAsync(int id, UpdateParentDto updateParentDto);
        public Task<Parent?> DeleteAsync(int id);
        public Task<bool> NumeroParentExistAsync(string telephone);
    }
}