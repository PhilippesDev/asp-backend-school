using api_gestion_ecole.Dtos.Option_;
using api_gestion_ecole.Helpers;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Interfaces
{
    public interface IOptionRepository
    {
        public Task<List<Option>> GetAllAsync();
        public Task<Option?> GetByIdAsync(int id);
        public Task<Option?> CreateAsync(CreateOptionDto createOptionDto);
        public Task<Option?> UpdateAsync(int id, UpdateOptionDto updateOptionDto);
        public Task<Option?> DeleteAsync(int id);
        public Task<int> GetNombreOptionsAsync();
        public Task<int?> GetNombreClassesAsync(int optionId);
        public Task<int?> GetEffectifAsync(int optionId, string anneeScolaireDesignation);
        public Task<List<OptionWithEffectifDto>?> GetEffectifParOptionAsync(string anneeScolaireDesignation, QueryObject queryObject);
    }
}