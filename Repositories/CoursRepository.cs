using api_gestion_ecole.Data;
using api_gestion_ecole.Dtos.Cours;
using api_gestion_ecole.Helpers;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using api_gestion_ecole.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_gestion_ecole.Repositories
{
    public class CoursRepository:ICoursRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CoursRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Cours?> CreateAsync(CreateCoursDto createCoursDto)
        {
            await _dbContext.Cours.AddAsync(createCoursDto.ToCoursFromCreate());
            await _dbContext.SaveChangesAsync();
            return createCoursDto.ToCoursFromCreate() ?? null;
        }

        public async Task<Cours?> DeleteAsync(int id)
        {
            var option = await _dbContext.Cours.FirstOrDefaultAsync(o=>o.Id == id);
            if(option == null) return null;
            _dbContext.Remove(option);
            await _dbContext.SaveChangesAsync();
            return option;
        }

        public async Task<List<Cours>> GetAllAsync()
        {
            return await  _dbContext.Cours.ToListAsync();
        }

        public async Task<Cours?> GetByIdAsync(int id)
        {
            var option = await _dbContext.Cours.FirstOrDefaultAsync(o => o.Id == id);
            return option ?? null;
        }

        public async Task<Cours?> UpdateAsync(int id, UpdateCoursDto updateCoursDto)
        {
            var cours = await _dbContext.Cours.FirstOrDefaultAsync(o=>o.Id == id);
            if(cours == null) return null;
            cours.Designation = updateCoursDto.Designation;
            cours.Abreviation = updateCoursDto.Abreviation;
            await _dbContext.SaveChangesAsync();
            return cours;
        }

        public async Task<int> GetNombreCoursAsync()
        {
            return await _dbContext.Cours.CountAsync();
        }

        public async Task<int?> GetNombreClassesConcerneesAsync(int coursId, string anneeScolaireDesignation)
        {
            var cours = await _dbContext.Cours.FirstOrDefaultAsync(c => c.Id == coursId);
            if (cours == null) return null;

            var anneeScolaire = await AnneeScolaireResolver.ResolveAsync(_dbContext, anneeScolaireDesignation);
            if (anneeScolaire == null) return null;

            return await _dbContext.CoursConcernerClasse
                .CountAsync(c => c.CoursId == coursId && c.AnneeScolaireId == anneeScolaire.Id);
        }

        public async Task<List<CoursConcernerClasse>?> GetListedeCoursPourClasseAsync(int classeId, string anneeScolaireDesignation)
        {
            var classe = await _dbContext.Classe.FirstOrDefaultAsync(c => c.Id == classeId);
            if (classe == null) return null;

            var anneeScolaire = await AnneeScolaireResolver.ResolveAsync(_dbContext, anneeScolaireDesignation);
            if (anneeScolaire == null) return null;

            var coursConcernerClasse = _dbContext.CoursConcernerClasse
                                            .Include(c=>c.Cours)
                                            .Include(c=>c.Classe).ThenInclude(c=>c!.Option)
                                            .Include(c=>c.AnneeScolaire)
                                            .Include(c=>c.Enseignant)
                                            .AsQueryable();

            var cours = coursConcernerClasse.Where(c=> c.Classe == classe && c.AnneeScolaire == anneeScolaire); 
            return await cours.ToListAsync();
        }
    }
}