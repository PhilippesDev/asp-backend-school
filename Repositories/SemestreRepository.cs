using api_gestion_ecole.Data;
using api_gestion_ecole.Dtos.Semestre;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using api_gestion_ecole.Models;
using Microsoft.EntityFrameworkCore;

namespace api_gestion_ecole.Repositories
{
    public class SemestreRepository : ISemestreRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public SemestreRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Semestre?> CreateAsync(CreateSemestreDto createSemestreDto)
        {
            var semestre = createSemestreDto.ToSemestreFromCreate();
            await _dbContext.Semestre.AddAsync(semestre);
            await _dbContext.SaveChangesAsync();
            return semestre;
        }

        public async Task<Semestre?> DeleteAsync(int id)
        {
            var semestre = await _dbContext.Semestre.FirstOrDefaultAsync(x=>x.Id == id);
            if(semestre == null) return null;
            _dbContext.Remove(semestre);
            await _dbContext.SaveChangesAsync();
            return semestre;
        }

        public async Task<List<Semestre>> GetAllAsync()
        {
            return await _dbContext.Semestre.ToListAsync();
        }

        public async Task<Semestre?> GetByIdAsync(int id)
        {
            var semestre = await _dbContext.Semestre.FirstOrDefaultAsync(s => s.Id == id);
            return semestre ?? null;
        }

        public async Task<Semestre?> UpdateAsync(int id, UpdateSemestreDto updateSemestreDto)
        {
            var semestre = await _dbContext.Semestre.FirstOrDefaultAsync(x=>x.Id == id);
            if(semestre == null) return null;
            semestre.Designation = updateSemestreDto.Designation;
            await _dbContext.SaveChangesAsync();
            return semestre;
        }
    }
}