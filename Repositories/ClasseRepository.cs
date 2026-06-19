using api_gestion_ecole.Data;
using api_gestion_ecole.Dtos.Classe;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using api_gestion_ecole.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api_gestion_ecole.Repositories
{
    public class ClasseRepository : IClasseRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ClasseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Classe?> CreateAsync(CreateClasseDto createClasseDto)
        {
            await _dbContext.Classe.AddAsync(createClasseDto.ToClasseFromCreate());
            await _dbContext.SaveChangesAsync();
            return createClasseDto.ToClasseFromCreate() ?? null;
        }

        public async Task<Classe?> DeleteAsync(int id)
        {
            var classe = await _dbContext.Classe.FirstOrDefaultAsync(x=>x.Id == id);
            if(classe == null) return null;
            _dbContext.Remove(classe);
            await _dbContext.SaveChangesAsync();
            return classe;
        }

        public async Task<List<Classe>> GetAllAsync()
        {
            return await _dbContext.Classe.Include(c=>c.Option).ToListAsync();
        }

        public async Task<Classe?> GetByIdAsync(int id)
        {
            var classe = await _dbContext.Classe.Include(c=>c.Option).FirstOrDefaultAsync(o => o.Id == id);
            return classe ?? null;
        }

        public async Task<Classe?> UpdateAsync(int id, UpdateClasseDto updateClasseDto)
        {
            var classe = await _dbContext.Classe.Include(c=>c.Option).FirstOrDefaultAsync(c=>c.Id == id);
            if(classe == null) return null;
            classe.Designation = updateClasseDto.Designation;
            classe.OptionId = updateClasseDto.OptionId;
            await _dbContext.SaveChangesAsync();
            return classe;
        }

        public async Task<bool> IsOptionExitAsync(int id)
        {
            var option = await _dbContext.Option.FirstOrDefaultAsync(o=>o.Id == id);
            if(option != null) return true;
            return false;
        }
    }
}