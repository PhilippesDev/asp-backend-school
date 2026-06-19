using api_gestion_ecole.Dtos.Periode;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Interfaces
{
    public interface IPeriodeRepository
    {
        public Task<List<Periode>> GetAllAsync();
        public Task<Periode?> GetByIdAsync(int id);
        public Task<Periode?> CreateAsync(CreatePeriodeDto createPeriodeDto);
        public Task<Periode?> UpdateAsync(int id, UpdatePeriodeDto updatePeriodeDto);
        public Task<Periode?> DeleteAsync(int id);
    }
}