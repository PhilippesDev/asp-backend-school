using api_gestion_ecole.Data;
using api_gestion_ecole.Dtos.Cotation;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using api_gestion_ecole.Models;
using Microsoft.EntityFrameworkCore;

namespace api_gestion_ecole.Repositories
{
    public class CotationRepository:ICotationRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CotationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Cotation?> CreateAsync(CreateCotationDto createCotationDto)
        {
            var inscription = await _dbContext.Inscription.Include(i=>i.Classe).FirstOrDefaultAsync(i=>i.Id == createCotationDto.InscriptionId);
            var coursConcernerClasse = await _dbContext.CoursConcernerClasse.Include(i=>i.Classe).FirstAsync(c=>c.Id == createCotationDto.CoursConcernerClasseId);
            if(inscription == null || coursConcernerClasse == null)
                return null;
            
            if((inscription.Classe != coursConcernerClasse.Classe!) ||
                (inscription.AnneeScolaire != coursConcernerClasse.AnneeScolaire)
              )
            {
                return null;
            }
               
            if(createCotationDto.Cote > coursConcernerClasse.Max)
                return null;

            var cotation = createCotationDto.ToCotationFromCreate();
            await _dbContext.Cotation.AddAsync(cotation);
            await _dbContext.SaveChangesAsync();
            return cotation;
        }

        public async Task<Cotation?> DeleteAsync(int id)
        {
            var cotation  = await _dbContext.Cotation
                                    .FirstOrDefaultAsync(i=>i.Id == id);
            if(cotation == null)
                return null;
            _dbContext.Remove(cotation);
            await _dbContext.SaveChangesAsync();
            return cotation;
        }
        
        public async Task<List<Cotation>> GetAllAsync()
        {
            return await _dbContext.Cotation
                        .Include(c=>c.CoursConcernerClasse)
                            .ThenInclude(c=>c.Cours)
                        .Include(c=>c.CoursConcernerClasse)
                            .ThenInclude(c=>c.Classe)
                                .ThenInclude(c=>c!.Option)
                        .Include(c=>c.Inscription)
                            .ThenInclude(i=>i!.Eleve)
                        .Include(c=>c.Periode)
                    .ToListAsync();
        }

        public async Task<Cotation?> GetByIdAsync(int id)
        {
            var cotation  = await _dbContext.Cotation
                        .Include(c=>c.CoursConcernerClasse)
                            .ThenInclude(c=>c.Cours)
                        .Include(c=>c.CoursConcernerClasse)
                            .ThenInclude(c=>c.Classe)
                                .ThenInclude(c=>c!.Option)
                        .Include(c=>c.Inscription)
                            .ThenInclude(i=>i!.Eleve)
                        .Include(c=>c.Periode)
                    .FirstOrDefaultAsync(c=>c.Id == id);
                    
            return cotation;
        }

        public async Task<bool> IsCotationExistAsync(int inscriptionId, 
                    int coursConcernerClasseId, int periodeId)
        {
            var cotation = await _dbContext.Cotation.FirstOrDefaultAsync(i=>
                            i.InscriptionId == inscriptionId && 
                            i.CoursConcernerClasseId == coursConcernerClasseId &&
                            i.PeriodeId == periodeId);

            return cotation != null;
        }

        public async Task<bool> IsCoursConcernerClasseExistAsync(int id)
        {
            var coursConcernerClasse = await _dbContext.CoursConcernerClasse
                                            .FirstOrDefaultAsync(i=>i.Id == id);
            return coursConcernerClasse != null;
        }

        public async Task<bool> IsInscriptionExistAsync(int id)
        {
            var inscription = await _dbContext.Inscription
                                            .FirstOrDefaultAsync(i=>i.Id == id);
            return inscription != null;
        }

        public async Task<bool> IsPeriodeExistAsync(int id)
        {
            var periode = await _dbContext.Periode.FirstOrDefaultAsync(i=>i.Id == id);
            return periode != null;
        }

        public async Task<Cotation?> UpdateAsync(int id, UpdateCotationDto updateCotationDto)
        {
            var cotation = await _dbContext.Cotation
                            .FirstOrDefaultAsync(i=>i.Id == id);
        
            if(cotation == null) return null;

            cotation.Cote = updateCotationDto.Cote;
            await _dbContext.SaveChangesAsync();
            return cotation;
        }

        public async Task<string?> ErrorMessageCheckCoteWhenUpdating(int cotationId, double cote)
        {
            var cotation = await _dbContext.Cotation.Include(c=>c.CoursConcernerClasse)
                            .FirstOrDefaultAsync(i=>i.Id == cotationId);
            if(cotation == null) return "not found";

            if(cote > cotation.CoursConcernerClasse.Max)
                return "La cote ne doit pas dépassé le maximum";
            return null;
        }
    }
}