using api_gestion_ecole.Data;
using api_gestion_ecole.Dtos.Frais;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using api_gestion_ecole.Models;
using Microsoft.EntityFrameworkCore;

namespace api_gestion_ecole.Repositories
{
    public class FraisRepository : IFraisRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public FraisRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Frais?> CreateAsync(CreateFraisDto createFraisDto)
        {
            await _dbContext.Frais.AddAsync(createFraisDto.ToFraisFromCreate());
            await _dbContext.SaveChangesAsync();
            return createFraisDto.ToFraisFromCreate() ?? null;
        }

        public async Task<Frais?> DeleteAsync(int id)
        {
            var frais = await _dbContext.Frais.FirstOrDefaultAsync(x=>x.Id == id);
            if(frais == null) return null;
            _dbContext.Remove(frais);
            await _dbContext.SaveChangesAsync();
            return frais;
        }

        public async Task<List<Frais>> GetAllAsync()
        {
            return await _dbContext.Frais.Include(f=>f.CategorieFrais).ToListAsync();
        }

        public async Task<Frais?> GetByIdAsync(int id)
        {
            var frais = await _dbContext.Frais.Include(f=>f.CategorieFrais).FirstOrDefaultAsync(f => f.Id == id);
            return frais ?? null;
        }

        public async Task<Frais?> UpdateAsync(int id, UpdateFraisDto updateFraisDto)
        {
            var frais = await _dbContext.Frais.Include(f=>f.CategorieFrais).FirstOrDefaultAsync(o=>o.Id == id);
            if(frais == null) return null;
            frais.Designation = updateFraisDto.Designation;
            frais.CategorieFraisId = updateFraisDto.CategorieFraisId;
            await _dbContext.SaveChangesAsync();
            return frais;
        }

        public async Task<bool> IsCategorieFraisExistAsync(int id)
        {
            var categorieFrais = await _dbContext.CategorieFrais.FirstOrDefaultAsync(c=>c.Id == id);
            if(categorieFrais != null) return true;
            return false;
        }
    }
}