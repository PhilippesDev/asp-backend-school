using api_gestion_ecole.Data;
using api_gestion_ecole.Dtos.Insciption;
using api_gestion_ecole.Helpers;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using api_gestion_ecole.Models;
using Microsoft.EntityFrameworkCore;

namespace api_gestion_ecole.Repositories
{
    public class InscriptionRepository : IInscriptionRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public InscriptionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Inscription?> CreateAsync(CreateInscriptionDto createInscriptionDto)
        {
            await _dbContext.Inscription.AddAsync(createInscriptionDto.ToInscriptionFromCreate());
            await _dbContext.SaveChangesAsync();
            return createInscriptionDto.ToInscriptionFromCreate();
        }

        public async Task<List<Inscription>> GetAllAsync(QueryObject queryObject)
        {
            var inscription = _dbContext.Inscription
                    .Include(i=>i.Eleve)
                    .Include(i=>i.AnneeScolaire)
                    .Include(i=>i.Classe).ThenInclude(c=>c!.Option).AsQueryable();
           
            if(!string.IsNullOrEmpty(queryObject.Designation))
                inscription = inscription.Where(c=>
                    c.Classe!.Designation!.ToLower()
                        .Contains(queryObject.Designation.ToLower()) || 
                         c.Eleve!.Nom.ToLower()
                        .Contains(queryObject.Designation.ToLower()) ||
                          c.Eleve!.Postnom.ToLower()
                        .Contains(queryObject.Designation.ToLower()) ||
                          c.Eleve!.Prenom.ToLower()
                        .Contains(queryObject.Designation.ToLower()) ||
                         c.Classe.Option.Designation.ToLower()
                        .Contains(queryObject.Designation.ToLower())
                     );
            
            if(queryObject.IsDescending == true) 
                inscription = inscription.OrderByDescending(c=>c.Id);

            int skip = (queryObject.Page - 1) * queryObject.PageSize; 
           
            inscription = inscription.Skip(skip).Take(queryObject.PageSize);

            return await inscription.ToListAsync();
        }

        public async Task<Inscription?> GetByIdAsync(int id)
        {
            var inscription = await _dbContext.Inscription
                    .Include(i=>i.Eleve)
                    .Include(i=>i.AnneeScolaire)
                    .Include(i=>i.Classe).ThenInclude(c=>c!.Option)
                    .FirstOrDefaultAsync(i=>i.Id == id);
            return inscription ?? null;
        }

        public async Task<bool> IsAnneeScolaireExistAsync(int id)
        {
            var annee_scolaire = await _dbContext.AnneeScolaire.FirstOrDefaultAsync(a=>a.Id == id);
            if(annee_scolaire != null)
                return true;
            return false;
        }

        public async Task<bool> IsClasseExistAsync(int id)
        {
            var classe = await _dbContext.Classe.FirstOrDefaultAsync(c=>c.Id == id);
            if(classe != null) return true;
            return false;
        }

        public async Task<bool> IsEleveExistAsync(int id)
        {
            var eleve = await _dbContext.Eleve.FirstOrDefaultAsync(e=>e.Id == id);
            if(eleve != null) return true;
            return false;
        }

        public async Task<bool> IsEleveAlreadyInscritAsync(int EleveId, int AnneeScolaireId)
        {
            var eleveInscit = await _dbContext.Inscription.FirstOrDefaultAsync(i=>
                i.EleveId == EleveId && i.AnneeScolaireId == AnneeScolaireId
            );
            return eleveInscit != null;
        }

        public async Task<Inscription?> UpdateAsync(int inscriptionId, UpdateInscriptionDto updateInscriptionDto)
        {
            var inscription = await _dbContext.Inscription
                .FirstOrDefaultAsync(i=>i.Id == inscriptionId);

            if(inscription == null) return null;

            inscription.ClasseId = updateInscriptionDto.ClasseId;
            await _dbContext.SaveChangesAsync();
            return inscription;
        }
        public async Task<Inscription?> DeleteAsync(int id)
        {
            var inscription = await _dbContext.Inscription.
                        FirstOrDefaultAsync(i=>i.Id == id);
                
            if(inscription == null) return null;

            _dbContext.Inscription.Remove(inscription);
            await _dbContext.SaveChangesAsync();
            return inscription;
        }

        public async Task<int?> GetNombreInscriptionsAsync(string anneeScolaireDesignation)
        {
            var anneeScolaire = await AnneeScolaireResolver.ResolveAsync(_dbContext, anneeScolaireDesignation);
            if (anneeScolaire == null) return null;

            return await _dbContext.Inscription
                .CountAsync(i => i.AnneeScolaireId == anneeScolaire.Id);
        }

    }
}