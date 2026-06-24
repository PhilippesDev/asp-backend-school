using api_gestion_ecole.Dtos.Cotation;
using api_gestion_ecole.Helpers;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Interfaces
{
    public interface ICotationRepository
    {
        public Task<List<Cotation>> GetAllAsync(QueryObject queryObject);
        public Task<Cotation?> GetByIdAsync(int id);
        public Task<Cotation?> CreateAsync(CreateCotationDto createCotationDto);
        public Task<Cotation?> UpdateAsync(int id, UpdateCotationDto updateCotationDto);
        public Task<Cotation?> DeleteAsync(int id);
        public Task<bool> IsCotationExistAsync(int inscriptionId, int coursConcernerClasseId, int periodeId);
        public Task<bool> IsInscriptionExistAsync(int id);
        public Task<bool> IsCoursConcernerClasseExistAsync(int id);
        public Task<bool> IsPeriodeExistAsync(int id);
        public Task<string?> ErrorMessageCheckCoteWhenUpdating(int cotationId, double cote);
        public Task<int> GetNombreCotationsAsync();
    }
}