using api_gestion_ecole.Data;
using api_gestion_ecole.Dtos.Enseignant;
using api_gestion_ecole.Helpers;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using api_gestion_ecole.Models;
using Microsoft.EntityFrameworkCore;

namespace api_gestion_ecole.Repositories
{
    public class EnseignantRepository : IEnseignantRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EnseignantRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Enseignant?> CreateAsync(CreateEnseignantDto createEnseignantDto)
        {
            var enseignant = createEnseignantDto.ToEnsiegnantFromCreate();
            await _dbContext.Enseignant.AddAsync(enseignant);
            await _dbContext.SaveChangesAsync();
            return enseignant ?? null;
        }
        public async Task<Enseignant?> DeleteAsync(int id)
        {
            var enseignant = await _dbContext.Enseignant.FirstOrDefaultAsync(x=>x.Id == id);
            if(enseignant == null) return null;
            _dbContext.Remove(enseignant);
            await _dbContext.SaveChangesAsync();
            return enseignant;
        }
        public async Task<List<Enseignant>> GetAllAsync(QueryObjectForPeople queryObject)
        {
            var enseignants =  _dbContext.Enseignant.AsQueryable();
        
            if(!string.IsNullOrEmpty(queryObject.Noms))
                enseignants = enseignants.Where(c=>
                    c.Nom!.ToLower()
                        .Contains(queryObject.Noms.ToLower()) || 
                     c.Postnom!.ToLower()
                        .Contains(queryObject.Noms.ToLower()) ||
                    c.Prenom!.ToLower()
                        .Contains(queryObject.Noms.ToLower()) ||
                    c.Specialite!.ToLower()
                        .Contains(queryObject.Noms.ToLower())
                     );
            
            if(queryObject.IsDescending == true) 
                enseignants = enseignants.OrderByDescending(c=>c.Id);

            int skip = (queryObject.Page - 1) * queryObject.PageSize; 
           
            enseignants = enseignants.Skip(skip).Take(queryObject.PageSize);

            return await enseignants.ToListAsync();
        }

        public async Task<Enseignant?> GetByIdAsync(int id)
        {
            return await _dbContext.Enseignant.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<int> GetNombreEnseignatsAsync()
        {
            return await _dbContext.Enseignant.CountAsync();
        }
        
        public async Task<Enseignant?> UpdateAsync(int id, UpdateEnseignantDto updateEnseignantDto)
        {
            var enseignant = await _dbContext.Enseignant.FirstOrDefaultAsync(x=>x.Id == id);
            if(enseignant == null) return null;
            enseignant.UpdateEnseignant(updateEnseignantDto);
            await _dbContext.SaveChangesAsync();
            return enseignant;
        }
    }
}