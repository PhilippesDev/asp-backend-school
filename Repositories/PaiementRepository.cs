using api_gestion_ecole.Data;
using api_gestion_ecole.Dtos.Paiement;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using api_gestion_ecole.Models;
using Microsoft.EntityFrameworkCore;

namespace api_gestion_ecole.Repositories
{
    public class PaiementRepository : IPaiementRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public PaiementRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Paiement?> CreateAsync(CreatePaiementDto createPaiementDto)
        {
            var inscription = await _dbContext.Inscription
                                    .Include(i=>i.Classe)
                                    .Include(i=>i.AnneeScolaire)
                                .FirstOrDefaultAsync(i=>
                                i.Id == createPaiementDto.InscriptionId);
            var fraisConcernerClasse = await _dbContext.FraisConcernerClasses
                                        .Include(f=>f.Classe)
                                        .Include(f=>f.AnneeScolaire)
                                    .FirstOrDefaultAsync(f=>
                                    f.Id == createPaiementDto.FraisConcernerClasseId);

            if(inscription == null || fraisConcernerClasse == null)
                    return null;
            
            if((inscription.Classe != fraisConcernerClasse!.Classe) || 
                (inscription.AnneeScolaire != fraisConcernerClasse.AnneeScolaire))
                    return null;

            var paiement = createPaiementDto.ToPaiementFromCreate(); 
            await _dbContext.Paiement.AddAsync(paiement);
            await _dbContext.SaveChangesAsync();
            return paiement;
        }

        public async Task<Paiement?> DeleteAsync(int id)
        {
            var paiement = await _dbContext.Paiement.FirstOrDefaultAsync(p=>p.Id == id);
            if(paiement == null) return null;

            _dbContext.Remove(paiement);
            await _dbContext.SaveChangesAsync();
            return paiement;
        }

        public async Task<List<Paiement>> GetAllAsync()
        {
            return await _dbContext.Paiement
                        .Include(p=>p.Inscription).ThenInclude(i=>i!.Eleve)
                        .Include(p=>p.FraisConcernerClasse).ThenInclude(f=>f!.Frais)
                        .Include(p=>p.FraisConcernerClasse).ThenInclude(f=>f!.Classe).ThenInclude(c=>c!.Option)
                        .ToListAsync();
        }

        public async Task<Paiement?> GetByIdAsync(int id)
        {
            return await _dbContext.Paiement
                        .Include(p=>p.Inscription).ThenInclude(i=>i!.Eleve)
                        .Include(p=>p.FraisConcernerClasse).ThenInclude(f=>f!.Frais)
                        .FirstOrDefaultAsync(p=>p.Id == id);
        }

        public async Task<Paiement?> UpdateAsync(int id, UpdatePaiementDto updatePaiementDto)
        {
            var paiement = await _dbContext.Paiement
                                .Include(p=>p.Inscription)
                                    .ThenInclude(i=>i!.Eleve)
                                .Include(p=>p.FraisConcernerClasse).ThenInclude(f=>f!.Frais)
                            .FirstOrDefaultAsync(p=>p.Id == id);

            if(paiement == null) return null;

            paiement.Montant = updatePaiementDto.Montant;
            await _dbContext.SaveChangesAsync();
            return paiement;
        }
        public async Task<bool> IsInscriptionExistAsync(int id)
        {
            var inscription = await _dbContext.Inscription.FirstOrDefaultAsync(i=>i.Id == id);
            if(inscription != null) return true;
            return false;
        }
        public async Task<bool> IsFraisConcernerClasseExistAsync(int id)
        {
            var frais = await _dbContext.FraisConcernerClasses.FirstOrDefaultAsync(f=>f.Id == id);
            return frais != null;
        }
    }
}