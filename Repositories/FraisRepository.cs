using api_gestion_ecole.Data;
using api_gestion_ecole.Dtos.Frais;
using api_gestion_ecole.Helpers;
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

        public async Task<List<Frais>> GetAllAsync(QueryObject queryObject)
        {
            var frais = _dbContext.Frais.Include(f=>f.CategorieFrais).AsQueryable();

        
            if(!string.IsNullOrEmpty(queryObject.Designation))
                frais = frais.Where(c=>
                    c.Designation!.ToLower()
                        .Contains(queryObject.Designation.ToLower()) 
                     );
            
            if(queryObject.IsDescending == true) 
                frais = frais.OrderByDescending(c=>c.Id);

            int skip = (queryObject.Page - 1) * queryObject.PageSize; 
           
            frais = frais.Skip(skip).Take(queryObject.PageSize);

            return await frais.ToListAsync();
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

        public async Task<int> GetNombreFraisAsync()
        {
            return await _dbContext.Frais.CountAsync();
        }

        public async Task<int?> GetNombreClassesConcerneesAsync(int fraisId, string anneeScolaireDesignation)
        {
            var frais = await _dbContext.Frais.FirstOrDefaultAsync(f => f.Id == fraisId);
            if (frais == null) return null;

            var anneeScolaire = await AnneeScolaireResolver.ResolveAsync(_dbContext, anneeScolaireDesignation);
            if (anneeScolaire == null) return null;

            return await _dbContext.FraisConcernerClasses
                .CountAsync(f => f.FraisId == fraisId && f.AnneeScolaireId == anneeScolaire.Id);
        }

        public async Task<List<FraisConcernerClasse>?> GetListedeFraisPourClasseAsync(int classeId, string anneeScolaireDesignation)
        {
            var classe = await _dbContext.Classe.FirstOrDefaultAsync(c => c.Id == classeId);
            if (classe == null) return null;

            var anneeScolaire = await AnneeScolaireResolver.ResolveAsync(_dbContext, anneeScolaireDesignation);
            if (anneeScolaire == null) return null;

            var fraisConcernerClasse = _dbContext.FraisConcernerClasses
                                            .Include(c=>c.Frais)
                                            .Include(c=>c.Classe).ThenInclude(c=>c!.Option)
                                            .Include(c=>c.AnneeScolaire)
                                            .AsQueryable();

            var frais = fraisConcernerClasse.Where(c=> c.Classe == classe && c.AnneeScolaire == anneeScolaire); 
            return await frais.ToListAsync();
        }
    }
}