using api_gestion_ecole.Data;
using api_gestion_ecole.Dtos;
using api_gestion_ecole.Dtos.AnneeScolaire;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using api_gestion_ecole.Models;
using Microsoft.EntityFrameworkCore;

namespace api_gestion_ecole.Repositories
{
    public class AnneeScolaireRepository : IAnneeScolaireRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public AnneeScolaireRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AnneeScolaire?> CreateAsync(CreateAnneeScolaireDto createAnneeScolaireDto)
        {
            await _dbContext.AnneeScolaire.AddAsync(createAnneeScolaireDto.ToAnneeScolaireFromCreate());
            await _dbContext.SaveChangesAsync();
            return createAnneeScolaireDto.ToAnneeScolaireFromCreate() ?? null;
        }

        public async Task<AnneeScolaire?> DeleteAsync(int id)
        {
            var annee = await _dbContext.AnneeScolaire.FirstOrDefaultAsync(a=>a.Id == id);
            if(annee == null) return null;
            _dbContext.Remove(annee);
            await _dbContext.SaveChangesAsync();
            return annee;
        }

        public async Task<List<AnneeScolaire>> GetAllAsync()
        {
            return await _dbContext.AnneeScolaire.ToListAsync();
        }

        public async Task<AnneeScolaire?> GetByIdAsync(int id)
        {
            var annee = await _dbContext.AnneeScolaire.FirstOrDefaultAsync(a=>a.Id == id);
            return annee ?? null;
        }

        public async Task<AnneeScolaire?> UpdateAsync(int id, UpdateAnneeScolaireDto updateAnneeScolaireDto)
        {
            var annee = await _dbContext.AnneeScolaire.FirstOrDefaultAsync(a=>a.Id == id);
            if(annee == null) return null;
            annee.Designation = updateAnneeScolaireDto.Designation;
            annee.DateDebut = updateAnneeScolaireDto.DateDebut;
            annee.DateFin = updateAnneeScolaireDto.DateFin;
        
            if(updateAnneeScolaireDto.EstActive == true)
            {
                if(await ActiveAnneeScolaireAsync(id) == null)
                    return null;
            }

            await _dbContext.SaveChangesAsync();
            return annee;
        }
        
        public async Task<AnneeScolaire?> ActiveAnneeScolaireAsync(int id)
        {
            var annnes = await _dbContext.AnneeScolaire.Where(a=>a.EstActive).ToListAsync();
            
            foreach(var an in annnes)
            {
                an.EstActive = false;
            }

            var annee = await _dbContext.AnneeScolaire.FindAsync(id);
            if(annee == null) return null;
            annee.EstActive = true;
            
            await _dbContext.SaveChangesAsync();
            return annee;
        }
        public async Task<bool> IsAnneeScolaireExist(int id, string designation)
        {
            var anneeScolaire = await _dbContext.AnneeScolaire.FirstOrDefaultAsync(a=>
                                    a.Designation.ToLower().Trim() == designation.ToLower().Trim());

            if(anneeScolaire != null && anneeScolaire.Id != id)
                    return true;

            return false;
        }
    }
}