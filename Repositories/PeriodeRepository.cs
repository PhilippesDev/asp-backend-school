using api_gestion_ecole.Data;
using api_gestion_ecole.Dtos.Periode;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using api_gestion_ecole.Models;
using Microsoft.EntityFrameworkCore;

namespace api_gestion_ecole.Repositories
{
    public class PeriodeRepository: IPeriodeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public PeriodeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Periode?> CreateAsync(CreatePeriodeDto createPeriodeDto)
        {
            await _dbContext.Periode.AddAsync(createPeriodeDto.ToPeriodeFromCreate());
            await _dbContext.SaveChangesAsync();
            return createPeriodeDto.ToPeriodeFromCreate() ?? null;
        }

        public async Task<Periode?> DeleteAsync(int id)
        {
            var periode = await _dbContext.Periode.FirstOrDefaultAsync(x=>x.Id == id);
            if(periode == null) return null;
            _dbContext.Remove(periode);
            await _dbContext.SaveChangesAsync();
            return periode;
        }

        public async Task<List<Periode>> GetAllAsync()
        {
            return await _dbContext.Periode.ToListAsync();
        }

        public async Task<Periode?> GetByIdAsync(int id)
        {
            var periode = await _dbContext.Periode.FirstOrDefaultAsync(o => o.Id == id);
            return periode ?? null;
        }

        public async Task<Periode?> UpdateAsync(int id, UpdatePeriodeDto updatePeriodeDto)
        {
            var periode = await _dbContext.Periode.FirstOrDefaultAsync(x=>x.Id == id);
            if(periode == null) return null;
            periode.Designation = updatePeriodeDto.Designation;
            await _dbContext.SaveChangesAsync();
            return periode;
        }
    }
}