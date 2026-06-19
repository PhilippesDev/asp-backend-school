using api_gestion_ecole.Data;
using api_gestion_ecole.Dtos.Option_;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using api_gestion_ecole.Models;
using Microsoft.EntityFrameworkCore;

namespace api_gestion_ecole.Repositories
{
    public class OptionRepository : IOptionRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public OptionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Option?> CreateAsync(CreateOptionDto createOptionDto)
        {
            await _dbContext.Option.AddAsync(createOptionDto.ToOptionFromCreate());
            await _dbContext.SaveChangesAsync();
            return createOptionDto.ToOptionFromCreate() ?? null;
        }

        public async Task<Option?> DeleteAsync(int id)
        {
            var option = await _dbContext.Option.FirstOrDefaultAsync(o=>o.Id == id);
            if(option == null) return null;
            _dbContext.Remove(option);
            await _dbContext.SaveChangesAsync();
            return option;
        }

        public async Task<List<Option>> GetAllAsync()
        {
            return await _dbContext.Option.ToListAsync();
        }

        public async Task<Option?> GetByIdAsync(int id)
        {
            var option = await _dbContext.Option.FirstOrDefaultAsync(o => o.Id == id);
            return option ?? null;
        }

        public async Task<Option?> UpdateAsync(int id, UpdateOptionDto updateOptionDto)
        {
            var option = await _dbContext.Option.FirstOrDefaultAsync(o=>o.Id == id);
            if(option == null) return null;
            option.Designation = updateOptionDto.Designation;
            option.Abreviation = updateOptionDto.Abreviation;
            await _dbContext.SaveChangesAsync();
            return option;
        }
    }
}