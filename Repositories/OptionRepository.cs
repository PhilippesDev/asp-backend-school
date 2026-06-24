using api_gestion_ecole.Data;
using api_gestion_ecole.Dtos.Option_;
using api_gestion_ecole.Helpers;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using api_gestion_ecole.Models;
using Microsoft.EntityFrameworkCore;

namespace api_gestion_ecole.Repositories
{
    public class OptionRepository : IOptionRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public OptionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Option?> CreateAsync(CreateOptionDto createOptionDto)
        {
            await _dbContext.Option.AddAsync(createOptionDto.ToOptionFromCreate());
            await _dbContext.SaveChangesAsync();
            return createOptionDto.ToOptionFromCreate() ?? null;
        }

        public async Task<Option?> DeleteAsync(int id)
        {
            var option = await _dbContext.Option.FirstOrDefaultAsync(o=>o.Id == id);
            if(option == null) return null;
            _dbContext.Remove(option);
            await _dbContext.SaveChangesAsync();
            return option;
        }

        public async Task<List<Option>> GetAllAsync()
        {
            return await _dbContext.Option.ToListAsync();
        }

        public async Task<Option?> GetByIdAsync(int id)
        {
            var option = await _dbContext.Option.FirstOrDefaultAsync(o => o.Id == id);
            return option ?? null;
        }

        public async Task<Option?> UpdateAsync(int id, UpdateOptionDto updateOptionDto)
        {
            var option = await _dbContext.Option.FirstOrDefaultAsync(o=>o.Id == id);
            if(option == null) return null;
            option.Designation = updateOptionDto.Designation;
            option.Abreviation = updateOptionDto.Abreviation;
            await _dbContext.SaveChangesAsync();
            return option;
        }

        public async Task<int> GetNombreOptionsAsync()
        {
            return await _dbContext.Option.CountAsync();
        }

        public async Task<int?> GetNombreClassesAsync(int optionId)
        {
            var option = await _dbContext.Option.FirstOrDefaultAsync(o => o.Id == optionId);
            if (option == null) return null;

            return await _dbContext.Classe.CountAsync(c => c.OptionId == optionId);
        }

        public async Task<int?> GetEffectifAsync(int optionId, string anneeScolaireDesignation)
        {
            var option = await _dbContext.Option.FirstOrDefaultAsync(o => o.Id == optionId);
            if (option == null) return null;

            var anneeScolaire = await AnneeScolaireResolver.ResolveAsync(_dbContext, anneeScolaireDesignation);
            if (anneeScolaire == null) return null;

            return await _dbContext.Inscription
                .CountAsync(i => i.AnneeScolaireId == anneeScolaire.Id
                    && i.Classe!.OptionId == optionId);
        }

        public async Task<List<OptionWithEffectifDto>?> GetEffectifParOptionAsync(
            string anneeScolaireDesignation, QueryObject queryObject)
        {
            var anneeScolaire = await AnneeScolaireResolver.ResolveAsync(_dbContext, anneeScolaireDesignation);
            if (anneeScolaire == null) return null;

            var options = _dbContext.Option
                .Select(o => new OptionWithEffectifDto
                {
                    Id = o.Id,
                    Designation = o.Designation,
                    Abreviation = o.Abreviation,
                    NombreClasses = o.Classes!.Count(),
                    Effectif = o.Classes!
                        .SelectMany(c => c.Insciptions!)
                        .Count(i => i.AnneeScolaireId == anneeScolaire.Id)
                });

            if (!string.IsNullOrEmpty(queryObject.Designation))
                options = options.Where(o => o.Designation.ToLower()
                    .Contains(queryObject.Designation.ToLower()));

            if (queryObject.IsDescending == true)
                options = options.OrderByDescending(o => o.Id);

            int skip = (queryObject.Page - 1) * queryObject.PageSize;
            options = options.Skip(skip).Take(queryObject.PageSize);

            return await options.ToListAsync();
        }
    }
}