using api_gestion_ecole.Data;
using api_gestion_ecole.Dtos.Cours;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using api_gestion_ecole.Models;
using Microsoft.EntityFrameworkCore;

namespace api_gestion_ecole.Repositories
{
    public class CoursRepository:ICoursRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CoursRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Cours?> CreateAsync(CreateCoursDto createCoursDto)
        {
            await _dbContext.Cours.AddAsync(createCoursDto.ToCoursFromCreate());
            await _dbContext.SaveChangesAsync();
            return createCoursDto.ToCoursFromCreate() ?? null;
        }

        public async Task<Cours?> DeleteAsync(int id)
        {
            var option = await _dbContext.Cours.FirstOrDefaultAsync(o=>o.Id == id);
            if(option == null) return null;
            _dbContext.Remove(option);
            await _dbContext.SaveChangesAsync();
            return option;
        }

        public async Task<List<Cours>> GetAllAsync()
        {
            return await _dbContext.Cours.ToListAsync();
        }

        public async Task<Cours?> GetByIdAsync(int id)
        {
            var option = await _dbContext.Cours.FirstOrDefaultAsync(o => o.Id == id);
            return option ?? null;
        }

        public async Task<Cours?> UpdateAsync(int id, UpdateCoursDto updateCoursDto)
        {
            var cours = await _dbContext.Cours.FirstOrDefaultAsync(o=>o.Id == id);
            if(cours == null) return null;
            cours.Designation = updateCoursDto.Designation;
            cours.Abreviation = updateCoursDto.Abreviation;
            await _dbContext.SaveChangesAsync();
            return cours;
        }
    }
}