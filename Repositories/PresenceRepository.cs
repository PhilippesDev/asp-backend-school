using api_gestion_ecole.Data;
using api_gestion_ecole.Dtos.Presence;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using api_gestion_ecole.Models;
using Microsoft.EntityFrameworkCore;

namespace api_gestion_ecole.Repositories
{
    public class PresenceRepository : IPresenceRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public PresenceRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Presence?> CreateAsync(CreatePresenceDto createPresenceDto)
        {
            var presenceExist = await PresenceExist(createPresenceDto.InscriptionId);

            if(presenceExist != null)
                return presenceExist;

            var presence = createPresenceDto.ToPresenceFromCreate();
            await _dbContext.Presence.AddAsync(presence);
            await _dbContext.SaveChangesAsync();
            return presence;
        }

        public async Task<Presence?> DeleteAsync(int id)
        {
            var presence = await _dbContext.Presence.FirstOrDefaultAsync(p=>p.Id == id);
            if(presence == null) return null;

            _dbContext.Remove(presence);
            await _dbContext.SaveChangesAsync();
            return presence;
        }

        public async Task<List<Presence>> GetAllAsync()
        {
            return await _dbContext.Presence
                            .Include(p=>p.Inscription)
                                .ThenInclude(i=>i!.Classe)
                                    .ThenInclude(c=>c!.Option)
                            .ToListAsync();
        }

        public async Task<Presence?> GetByIdAsync(int id)
        {
           return  await _dbContext.Presence
                            .Include(p=>p.Inscription)
                                    .ThenInclude(i=>i!.Classe)
                                        .ThenInclude(c=>c!.Option)
                            .FirstOrDefaultAsync(a=>a.Id == id);
        }

        public async Task<bool> IsInscriptionExist(int id)
        {
            var inscription = await _dbContext.Inscription.FirstOrDefaultAsync(i=>i.Id == id);
            if(inscription != null) return true;
            return false;
        }

        public async Task<Presence?> PresenceExist(int inscriptionId)
        {
            var presence = await _dbContext.Presence.FirstOrDefaultAsync(i=>i.InscriptionId == inscriptionId && 
                                        i.DatePresence == DateOnly.FromDateTime(DateTime.UtcNow));
            return presence;
        }

        public async Task<int> GetNombreELevesPresents()
        {
            var presences = _dbContext.Presence.Where( i=>
                                        i.DatePresence == DateOnly.FromDateTime(DateTime.UtcNow));
            return await presences.CountAsync();
        }

        public async Task<int> GetNombreELevesAbsents(int anneeScolaireId)
        {
            var total_eleves = await _dbContext.Inscription.CountAsync(i=>i.AnneeScolaireId == anneeScolaireId);
            return total_eleves - await GetNombreELevesPresents();
        }

        public async Task<int> GetNombreELevesPresentsInClasse(int classeId)
        {
            var presences = _dbContext.Presence.Include(p=>p.Inscription).AsQueryable()
                                .Where(i =>
                                    i.Inscription!.ClasseId == classeId &&
                                    i.DatePresence == DateOnly.FromDateTime(DateTime.UtcNow));
            return await presences.CountAsync();
        }

        public async Task<int> GetNombreELevesAbsentsInClasse(int classeId,  int anneeScolaireActiveId)
        {
            var total_eleves = await _dbContext.Inscription.CountAsync(i=>i.ClasseId == classeId && i.AnneeScolaireId == anneeScolaireActiveId);
            return total_eleves - await GetNombreELevesPresentsInClasse(classeId);
        }
    }
}