using api_gestion_ecole.Data;
using api_gestion_ecole.Dtos.CategorieFrais;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using api_gestion_ecole.Models;
using Microsoft.EntityFrameworkCore;

namespace api_gestion_ecole.Repositories
{
    public class CategorieFraisRepository : ICategorieFraisRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CategorieFraisRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<CategorieFrais?> CreateAsync(CreateCategorieFraisDto createCategorieFraisDto)
        {
            await _dbContext.CategorieFrais.AddAsync(createCategorieFraisDto.ToCategorieFraisFromCreate());
            await _dbContext.SaveChangesAsync();
            return createCategorieFraisDto.ToCategorieFraisFromCreate() ?? null;
        }

        public async Task<CategorieFrais?> DeleteAsync(int id)
        {
            var categorieFrais = await _dbContext.CategorieFrais.FirstOrDefaultAsync(x=>x.Id == id);
            if(categorieFrais == null) return null;
            _dbContext.Remove(categorieFrais);
            await _dbContext.SaveChangesAsync();
            return categorieFrais;
        }

        public async Task<List<CategorieFrais>> GetAllAsync()
        {
            return await _dbContext.CategorieFrais.ToListAsync();
        }

        public async Task<CategorieFrais?> GetByIdAsync(int id)
        {
            var categorieFrais = await _dbContext.CategorieFrais.FirstOrDefaultAsync(o => o.Id == id);
            return categorieFrais ?? null;
        }

        public async Task<CategorieFrais?> UpdateAsync(int id, UpdateCategorieFraisDto updateCategorieFraisDto)
        {
            var categorieFrais = await _dbContext.CategorieFrais.FirstOrDefaultAsync(o=>o.Id == id);
            if(categorieFrais == null) return null;
            categorieFrais.Designation = updateCategorieFraisDto.Designation;
            await _dbContext.SaveChangesAsync();
            return categorieFrais;
        }

        public async Task<int> GetNombreCategoriesAsync()
        {
            return await _dbContext.CategorieFrais.CountAsync();
        }

        public async Task<int?> GetNombreFraisAsync(int categorieFraisId)
        {
            var categorie = await _dbContext.CategorieFrais.FirstOrDefaultAsync(c => c.Id == categorieFraisId);
            if (categorie == null) return null;

            return await _dbContext.Frais.CountAsync(f => f.CategorieFraisId == categorieFraisId);
        }
    }
}