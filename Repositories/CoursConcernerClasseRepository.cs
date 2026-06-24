using api_gestion_ecole.Data;
using api_gestion_ecole.Dtos.CoursConcernerClasse;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using api_gestion_ecole.Models;
using Microsoft.EntityFrameworkCore;

namespace api_gestion_ecole.Repositories
{
    public class CoursConcernerClasseRepository : ICoursConcernerClasseRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CoursConcernerClasseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<CoursConcernerClasse?> CreateAsync(CreateCoursConcernerClasseDto createCoursConcernerClasseDto)
        {
            var coursConcernerClasseExist = await _dbContext.CoursConcernerClasse
                                                    .FirstOrDefaultAsync(i=> 
                                                    i.ClasseId == createCoursConcernerClasseDto.ClasseId &&
                                                    i.CoursId == createCoursConcernerClasseDto.CoursId &&
                                                    i.AnneeScolaireId == createCoursConcernerClasseDto.AnneeScolaireId);
            
            if(coursConcernerClasseExist != null) return null;
        
            var coursConcernerClasse = createCoursConcernerClasseDto.ToCoursConcernerFromCreate();
            await _dbContext.CoursConcernerClasse.AddAsync(coursConcernerClasse);
            await _dbContext.SaveChangesAsync();
            return coursConcernerClasse;
        }

        public async Task<CoursConcernerClasse?> DeleteAsync(int id)
        {
           var coursConcernerClasseExist = await _dbContext.CoursConcernerClasse
                                                    .FirstOrDefaultAsync(i=> i.Id == id);
            if(coursConcernerClasseExist == null) return null;
            _dbContext.CoursConcernerClasse.Remove(coursConcernerClasseExist);
            await _dbContext.SaveChangesAsync();
            return coursConcernerClasseExist;
        }

        public async Task<List<CoursConcernerClasse>> GetAllAsync()
        {
            return await _dbContext.CoursConcernerClasse
                            .Include(c=>c.Cours)
                            .Include(c=>c.Classe).ThenInclude(c=>c!.Option)
                            .Include(c=>c.AnneeScolaire)
                            .Include(c=>c.Enseignant)
                            .ToListAsync();
        }

        public async Task<CoursConcernerClasse?> GetByIdAsync(int id)
        {
            return await _dbContext.CoursConcernerClasse
                            .Include(c=>c.Cours)
                            .Include(c=>c.Classe).ThenInclude(c=>c!.Option)
                            .Include(c=>c.Enseignant)
                            .Include(c=>c.AnneeScolaire)
                            .FirstOrDefaultAsync(i=>i.Id == id);
        }

        public async Task<bool> IsClasseExistAsync(int id)
        {
            var classe = await _dbContext.Classe.FirstOrDefaultAsync(c=>c.Id == id);
            if(classe != null) return true;
            return false;
        }

        public async Task<bool> IsCoursExistAsync(int id)
        {
            var cours = await _dbContext.Cours.FirstOrDefaultAsync(i=>i.Id == id);
            return cours != null;
        }
        public async Task<bool> IsEnseignantExistAsync(int id)
        {
            var enseignant = await _dbContext.Enseignant.FirstOrDefaultAsync(e=>e.Id == id);
            return enseignant != null;
        }
        public async Task<bool> IsAnneeScolaireExistAsync(int id)
        {
            var annee_scolaire = await _dbContext.AnneeScolaire.FirstOrDefaultAsync(a=>a.Id == id);
            if(annee_scolaire != null)
                return true;
            return false;
        }
        public async Task<CoursConcernerClasse?> UpdateAsync(int id, UpdateCoursConcernerClasseDto updateCoursConcernerClasseDto)
        {
            var coursConcernerClasseExist = await _dbContext.CoursConcernerClasse
                                                    .FirstOrDefaultAsync(i=> i.Id == id);
                                                     
            if(coursConcernerClasseExist == null) return null;
            coursConcernerClasseExist.EnseignantId = updateCoursConcernerClasseDto.EnseignantId;
            coursConcernerClasseExist.Max = updateCoursConcernerClasseDto.Max;
            coursConcernerClasseExist.NombreHeures = updateCoursConcernerClasseDto.NombreHeures;
            await _dbContext.SaveChangesAsync();
            return coursConcernerClasseExist;
        }
    }
}