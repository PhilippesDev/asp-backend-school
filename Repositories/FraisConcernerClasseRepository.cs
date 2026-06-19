using api_gestion_ecole.Data;
using api_gestion_ecole.Dtos.FraisConcernerClasse;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using api_gestion_ecole.Models;
using Microsoft.EntityFrameworkCore;

namespace api_gestion_ecole.Repositories
{
    public class FraisConcernerClasseRepository : IFraisConcernerClasseRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public FraisConcernerClasseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<FraisConcernerClasse?> CreateAsync(CreateFraisConcernerClasseDto createFraisConcernerClasseDto)
        {
            var fraisConcernerClasseExist = await _dbContext.FraisConcernerClasses
                                                    .FirstOrDefaultAsync(f=> 
                                                    f.ClasseId == createFraisConcernerClasseDto.ClasseId &&
                                                    f.FraisId == createFraisConcernerClasseDto.FraisId &&
                                                    f.AnneeScolaireId == createFraisConcernerClasseDto.AnneeScolaireId
                                                    );
            
            if(fraisConcernerClasseExist != null) return null;
        
            var fraisConcernerClasse = createFraisConcernerClasseDto.ToFraisConcernerFromCreate();
            await _dbContext.FraisConcernerClasses.AddAsync(fraisConcernerClasse);
            await _dbContext.SaveChangesAsync();
            return fraisConcernerClasse;
        }

        public async Task<FraisConcernerClasse?> DeleteAsync(int FraisId, int ClasseId, int AnneeScolaireId)
        {
           var fraisConcernerClasseExist = await _dbContext.FraisConcernerClasses
                                                    .FirstOrDefaultAsync(i=> 
                                                    i.ClasseId == ClasseId &&
                                                    i.FraisId == FraisId &&
                                                    i.AnneeScolaireId == AnneeScolaireId);
            if(fraisConcernerClasseExist == null) return null;
            _dbContext.FraisConcernerClasses.Remove(fraisConcernerClasseExist);
            await _dbContext.SaveChangesAsync();
            return fraisConcernerClasseExist;
        }

        public async Task<List<FraisConcernerClasse>> GetAllAsync()
        {
            return await _dbContext.FraisConcernerClasses
                            .Include(c=>c.Frais)
                            .Include(c=>c.Classe).ThenInclude(c=>c!.Option)
                            .Include(c=>c.AnneeScolaire)
                            .ToListAsync();
        }

        public async Task<FraisConcernerClasse?> GetByIdAsync(int FraisId, int ClasseId, int anneeScolaireId)
        {
            return await _dbContext.FraisConcernerClasses
                            .Include(c=>c.Frais)
                            .Include(c=>c.Classe).ThenInclude(c=>c!.Option)
                            .Include(c=>c.AnneeScolaire)
                            .FirstOrDefaultAsync(i=> 
                                i.ClasseId == ClasseId &&
                                i.FraisId == FraisId &&
                                i.AnneeScolaireId == anneeScolaireId);
        }

        public async Task<bool> IsClasseExistAsync(int id)
        {
            var classe = await _dbContext.Classe.FirstOrDefaultAsync(c=>c.Id == id);
            if(classe != null) return true;
            return false;
        }

        public async Task<bool> IsFraisExistAsync(int id)
        {
            var frais = await _dbContext.Frais.FirstOrDefaultAsync(f=>f.Id == id);
            return frais != null;
        }
        public async Task<bool> IsAnneeScolaireExistAsync(int id)
        {
            var annee_scolaire = await _dbContext.AnneeScolaire.FirstOrDefaultAsync(a=>a.Id == id);
            if(annee_scolaire != null)
                return true;
            return false;
        }
        public async Task<FraisConcernerClasse?> UpdateAsync(int FraisId, int ClasseId, int AnneeScolaireId, UpdateFraisConcernerClasseDto updatefraisConcernerClasseDto)
        {
            var fraisConcernerClasseExist = await _dbContext.FraisConcernerClasses
                                                    .FirstOrDefaultAsync(i=> 
                                                    i.ClasseId == ClasseId &&
                                                    i.FraisId == FraisId &&
                                                    i.AnneeScolaireId == AnneeScolaireId);
            
            if(fraisConcernerClasseExist == null) return null;
            fraisConcernerClasseExist.Montant = updatefraisConcernerClasseDto.Montant;
            await _dbContext.SaveChangesAsync();
            return fraisConcernerClasseExist;
        }
    }
}