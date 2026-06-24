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
            return await _dbContext.Periode.Include(p=>p.Semestre).ToListAsync();
        }

        public async Task<Periode?> GetByIdAsync(int id)
        {
            var periode = await _dbContext.Periode.Include(p=>p.Semestre).FirstOrDefaultAsync(o => o.Id == id);
            return periode ?? null;
        }

        public async Task<Periode?> UpdateAsync(int id, UpdatePeriodeDto updatePeriodeDto)
        {
            var periode = await _dbContext.Periode.FirstOrDefaultAsync(x=>x.Id == id);
            if(periode == null) return null;
            periode.Designation = updatePeriodeDto.Designation;
            periode.Coefficient = updatePeriodeDto.Coefficient;
            periode.SemestreId = updatePeriodeDto.SemestreId;
            await _dbContext.SaveChangesAsync();
            return periode;
        }

        public async Task<int> GetNombrePeriodesAsync()
        {
            return await _dbContext.Periode.CountAsync();
        }

        public async Task<int?> GetNombreCotationsAsync(int periodeId)
        {
            var periode = await _dbContext.Periode.FirstOrDefaultAsync(p => p.Id == periodeId);
            if (periode == null) return null;

            return await _dbContext.Cotation.CountAsync(c => c.PeriodeId == periodeId);
        }

        public async Task<bool> IsSemestreExistAsync(int id)
        {
            var semestre = await _dbContext.Semestre.FirstOrDefaultAsync(s => s.Id == id);
            return semestre != null;
        }
    }
}