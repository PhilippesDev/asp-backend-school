using api_gestion_ecole.Data;
using api_gestion_ecole.Dtos.Niveau;
using api_gestion_ecole.Helpers;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using api_gestion_ecole.Models;
using Microsoft.EntityFrameworkCore;

namespace api_gestion_ecole.Repositories
{
    public class NiveauRepository : INiveauRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public NiveauRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Niveau?> CreateAsync(CreateNiveauDto createNiveauDto)
        {
            var niveau = createNiveauDto.ToNiveauFromCreate();
            await _dbContext.Niveau.AddAsync(niveau);
            await _dbContext.SaveChangesAsync();
            return niveau;
        }

        public async Task<Niveau?> DeleteAsync(int id)
        {
            var niveau = await _dbContext.Niveau.FirstOrDefaultAsync(n=>n.Id == id);
            if(niveau == null) return null;
            _dbContext.Remove(niveau);
            await _dbContext.SaveChangesAsync();
            return niveau;
        }

        public async Task<List<Niveau>> GetAllAsync()
        {
            return await _dbContext.Niveau.ToListAsync();
        }

        public async Task<Niveau?> GetByIdAsync(int id)
        {
            return await _dbContext.Niveau.FirstOrDefaultAsync(n => n.Id == id);
            
        }

        public async Task<Niveau?> UpdateAsync(int id, UpdateNiveauDto updateNiveauDto)
        {
            var niveau = await _dbContext.Niveau.FirstOrDefaultAsync(n =>n.Id == id);
            if(niveau == null) return null;
            niveau.Designation = updateNiveauDto.Designation;
    
            await _dbContext.SaveChangesAsync();
            return niveau;
        }

        public async Task<int> GetNombreNiveausAsync()
        {
            return await _dbContext.Niveau.CountAsync();
        }

        public async Task<int?> GetNombreClassesAsync(int niveauId)
        {
            var niveau = await _dbContext.Niveau.FirstOrDefaultAsync(o => o.Id == niveauId);
            if (niveau == null) return null;

            return await _dbContext.Classe.CountAsync(c => c.NiveauId == niveauId);
        }

        public async Task<int?> GetEffectifAsync(int niveauId, string anneeScolaireDesignation)
        {
            var niveau = await _dbContext.Option.FirstOrDefaultAsync(o => o.Id == niveauId);
            if (niveau == null) return null;

            var anneeScolaire = await AnneeScolaireResolver.ResolveAsync(_dbContext, anneeScolaireDesignation);
            if (anneeScolaire == null) return null;

            return await _dbContext.Inscription
                .CountAsync(i => i.AnneeScolaireId == anneeScolaire.Id
                    && i.Classe!.NiveauId == niveauId);
        }

    }
}