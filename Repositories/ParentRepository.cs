using api_gestion_ecole.Data;
using api_gestion_ecole.Dtos.Parent;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using api_gestion_ecole.Models;
using Microsoft.EntityFrameworkCore;

namespace api_gestion_ecole.Repositories
{
    public class ParentRepository : IParentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ParentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Parent?> CreateAsync(CreateParentDto createParentDto)
        {
            var parent = createParentDto.ToParentFromCreate();
            await _dbContext.Parent.AddAsync(parent);
            await _dbContext.SaveChangesAsync();
            return parent;
        }

        public async Task<Parent?> DeleteAsync(int id)
        {
            var parent  = _dbContext.Parent.FirstOrDefault(p=>p.Id == id);
            if(parent == null) return null;

            _dbContext.Remove(parent);
            await _dbContext.SaveChangesAsync();
            return parent;
        }

        public async Task<List<Parent>> GetAllAsync()
        {
            return  await _dbContext.Parent.ToListAsync();
        }

        public async Task<Parent?> GetByIdAsync(int id)
        {
            return await _dbContext.Parent.FirstOrDefaultAsync(p=>p.Id == id);
        }

        public async Task<bool> NumeroParentExistAsync(string telephone)
        {
            if(string.IsNullOrEmpty(telephone)) return false;
            var parent = await _dbContext.Parent.FirstOrDefaultAsync(p=>p.Telephone == telephone);
            if(parent != null) return true;
            return false;
        }

        public async Task<Parent?> UpdateAsync(int id, UpdateParentDto updateParentDto)
        {
            var parent  = await _dbContext.Parent.FirstOrDefaultAsync(p=>p.Id == id);
            if(parent == null) return null;
            parent.UpdateParent(updateParentDto);
            await _dbContext.SaveChangesAsync();
            return parent;
        }

    }
}