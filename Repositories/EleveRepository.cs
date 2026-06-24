using api_gestion_ecole.Data;
using api_gestion_ecole.Dtos.Classe;
using api_gestion_ecole.Dtos.Eleve;
using api_gestion_ecole.Helpers;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using api_gestion_ecole.Models;
using Microsoft.EntityFrameworkCore;

namespace api_gestion_ecole.Repositories
{
    public class EleveRepository: IEleveRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public EleveRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Eleve?> CreateAsync(CreateEleveDto createEleveDto)
        {
            await _dbContext.Eleve.AddAsync(createEleveDto.ToEleveFromCreate());
            await _dbContext.SaveChangesAsync();
            return createEleveDto.ToEleveFromCreate() ?? null;
        }

        public async Task<Eleve?> DeleteAsync(int id)
        {
            var eleve = await _dbContext.Eleve.FirstOrDefaultAsync(x=>x.Id == id);
            if(eleve == null) return null;
            _dbContext.Remove(eleve);
            await _dbContext.SaveChangesAsync();
            return eleve;
        }

        public async Task<List<Eleve>> GetAllAsync()
        {
            // var eleves = _dbContext.Eleve
            return await _dbContext.Eleve.ToListAsync();
        
            /* if(!string.IsNullOrEmpty(queryObject.Noms))
                eleves = eleves.Where(c=>
                    c.Nom!.ToLower()
                        .Contains(queryObject.Noms.ToLower()) || 
                     c.Postnom!.ToLower()
                        .Contains(queryObject.Noms.ToLower()) ||
                    c.Prenom!.ToLower()
                        .Contains(queryObject.Noms.ToLower())
                     );
            
            if(queryObject.IsDescending == true) 
                eleves = eleves.OrderByDescending(c=>c.Id); */

            /* int skip = (queryObject.Page - 1) * queryObject.PageSize; 
           
            eleves = eleves.Skip(skip).Take(queryObject.PageSize); */

            // return await eleves.ToListAsync();
        }

        public async Task<Eleve?> GetByIdAsync(int id)
        {
            var eleve = await _dbContext.Eleve.FirstOrDefaultAsync(o => o.Id == id);
            return eleve ?? null;
        }

        public async Task<Eleve?> UpdateAsync(int id, UpdateEleveDto updateEleveDto)
        {
            var eleve = await _dbContext.Eleve.FirstOrDefaultAsync(x=>x.Id == id);
            if(eleve == null) return null;
            eleve.UpdateEleve(updateEleveDto);
            await _dbContext.SaveChangesAsync();
            return eleve;
        }

        public async Task<int> GetNombreElevesAsync()
        {
            return await _dbContext.Eleve.CountAsync();
        }

        public async Task<int?> GetNombreInscriptionsAsync(int eleveId)
        {
            var eleve = await _dbContext.Eleve.FirstOrDefaultAsync(e => e.Id == eleveId);
            if (eleve == null) return null;

            return await _dbContext.Inscription.CountAsync(i => i.EleveId == eleveId);
        }
    }
}