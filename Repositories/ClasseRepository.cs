using api_gestion_ecole.Data;
using api_gestion_ecole.Dtos.Classe;
using api_gestion_ecole.Helpers;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using api_gestion_ecole.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api_gestion_ecole.Repositories
{
    public class ClasseRepository : IClasseRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ClasseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Classe?> CreateAsync(CreateClasseDto createClasseDto)
        {
            await _dbContext.Classe.AddAsync(createClasseDto.ToClasseFromCreate());
            await _dbContext.SaveChangesAsync();
            return createClasseDto.ToClasseFromCreate() ?? null;
        }

        public async Task<Classe?> DeleteAsync(int id)
        {
            var classe = await _dbContext.Classe.FirstOrDefaultAsync(x=>x.Id == id);
            if(classe == null) return null;
            _dbContext.Remove(classe);
            await _dbContext.SaveChangesAsync();
            return classe;
        }

        public async Task<List<Classe>> GetAllAsync(QueryObject queryObject)
        {
            var classe = _dbContext.Classe.Include(c=>c.Option).AsQueryable();

            if(!string.IsNullOrEmpty(queryObject.Designation))
                classe = classe.Where(c=>c.Designation.ToLower()
                    .Contains(queryObject.Designation.ToLower()));
            
            if(queryObject.IsDescending == true) 
                classe = classe.OrderByDescending(c=>c.Id);

            int skip = (queryObject.Page - 1) * queryObject.PageSize; 
           
            classe = classe.Skip(skip).Take(queryObject.PageSize);
            
            return await classe.ToListAsync();
        }

        public async Task<Classe?> GetByIdAsync(int id)
        {
            var classe = await _dbContext.Classe.Include(c=>c.Option).FirstOrDefaultAsync(o => o.Id == id);
            return classe ?? null;
        }

        public async Task<Classe?> UpdateAsync(int id, UpdateClasseDto updateClasseDto)
        {
            var classe = await _dbContext.Classe.Include(c=>c.Option).FirstOrDefaultAsync(c=>c.Id == id);
            if(classe == null) return null;
            classe.Designation = updateClasseDto.Designation;
            classe.OptionId = updateClasseDto.OptionId;
            await _dbContext.SaveChangesAsync();
            return classe;
        }

        public async Task<bool> IsOptionExitAsync(int id)
        {
            var option = await _dbContext.Option.FirstOrDefaultAsync(o=>o.Id == id);
            if(option != null) return true;
            return false;
        }

        public async Task<int> GetNombreClasseAsync()
        {
            return await _dbContext.Classe.CountAsync();
        }

        public async Task<int?> GetNombreEleveInClasseAsync(int classeId, string anneeScolaireDesignation)
        {
            var classe = await _dbContext.Classe.FirstOrDefaultAsync(c=>c.Id == classeId);
            if(classe == null) return null;

            if(!string.IsNullOrEmpty(anneeScolaireDesignation))
            {
                var anneeScolaire = await _dbContext.AnneeScolaire
                                        .FirstOrDefaultAsync(a=>a.Designation == anneeScolaireDesignation);
                if(anneeScolaire == null) return null;

                var inscriptions = _dbContext.Inscription.AsQueryable();
                inscriptions = inscriptions.Where(i=>i.ClasseId == classeId 
                                                && i.AnneeScolaireId == anneeScolaire.Id);
                return await inscriptions.CountAsync();
            }
            else
            {
                var anneeScolaireActive = await _dbContext.AnneeScolaire
                                    .FirstOrDefaultAsync(a=>a.EstActive == true);

                if(anneeScolaireActive == null) return null;

                var inscriptions = _dbContext.Inscription.AsQueryable();
                inscriptions = inscriptions.Where(i=>i.ClasseId == classeId 
                                                && i.AnneeScolaireId == anneeScolaireActive.Id);
                return await inscriptions.CountAsync();
            }       
        }

        public async Task<List<ClasseWithNombreEleves>?> GetNombreEleveParClasseAsync(string anneeScolaireDesignation,QueryObject queryObject)
        {

            if(!string.IsNullOrEmpty(anneeScolaireDesignation))
            {
                var anneeScolaire = await _dbContext.AnneeScolaire
                                        .FirstOrDefaultAsync(a=>a.Designation == anneeScolaireDesignation);
                if(anneeScolaire == null) return null;

                 var classes = _dbContext.Classe
                                .Select(c=>
                                    new ClasseWithNombreEleves()
                                    {
                                        Id = c.Id,
                                        OptionId = c.OptionId,
                                        Designation = c.Designation,
                                        Option = c.Option.Designation,
                                        Effectif =c.Insciptions!.Count(i=>i.AnneeScolaire!.Designation == anneeScolaireDesignation)
                                    }
                );

                if(!string.IsNullOrEmpty(queryObject.Designation))
                    classes = classes.Where(c=>c.Designation.ToLower()
                    .Contains(queryObject.Designation.ToLower()));
                
                if(queryObject.IsDescending == true) 
                    classes = classes.OrderByDescending(c=>c.Id);

                int skip = (queryObject.Page - 1) * queryObject.PageSize; 
            
                classes = classes.Skip(skip).Take(queryObject.PageSize);
                    return await classes.ToListAsync();

            }
            else
            {
                var anneeScolaireActive = await _dbContext.AnneeScolaire
                                    .FirstOrDefaultAsync(a=>a.EstActive == true);

                if(anneeScolaireActive == null) return null;

                var classes = _dbContext.Classe
                                .Select(c=>
                                    new ClasseWithNombreEleves()
                                    {
                                        Id = c.Id,
                                        OptionId = c.OptionId,
                                        Designation = c.Designation,
                                        Option = c.Option.Designation,
                                        Effectif =c.Insciptions!.Count(i=>i.AnneeScolaire!.Designation == anneeScolaireActive.Designation)
                                    }    
                );

                if(!string.IsNullOrEmpty(queryObject.Designation))
                    classes = classes.Where(c=>c.Designation.ToLower()
                    .Contains(queryObject.Designation.ToLower()));
                
                if(queryObject.IsDescending == true) 
                    classes = classes.OrderByDescending(c=>c.Id);

                int skip = (queryObject.Page - 1) * queryObject.PageSize; 
            
                classes = classes.Skip(skip).Take(queryObject.PageSize);

                return await classes.ToListAsync();
            } 
        }

        public async Task<int?> GetNombreCoursInClasseAsync(int classeId, string anneeScolaireDesignation)
        {
            var classe = await _dbContext.Classe.FirstOrDefaultAsync(c=>c.Id == classeId);
            if(classe == null) return null;

            if(!string.IsNullOrEmpty(anneeScolaireDesignation))
            {
                var anneeScolaire = await _dbContext.AnneeScolaire
                                        .FirstOrDefaultAsync(a=>a.Designation == anneeScolaireDesignation);
                if(anneeScolaire == null) return null;

                var coursConcernerClasses = _dbContext.CoursConcernerClasse.AsQueryable();
                coursConcernerClasses = coursConcernerClasses.Where(i=>i.ClasseId == classeId 
                                                && i.AnneeScolaireId == anneeScolaire.Id);
                return await coursConcernerClasses.CountAsync();
            }
            else
            {
                var anneeScolaireActive = await _dbContext.AnneeScolaire
                                    .FirstOrDefaultAsync(a=>a.EstActive == true);

                if(anneeScolaireActive == null) return null;

                var coursConcernerClasses = _dbContext.CoursConcernerClasse.AsQueryable();
                coursConcernerClasses = coursConcernerClasses.Where(i=>i.ClasseId == classeId 
                                                && i.AnneeScolaireId == anneeScolaireActive.Id);
                return await coursConcernerClasses.CountAsync();
            }       
        }

        public async Task<decimal?> GetMontantAPayerInClasseAsync(int classeId, string anneeScolaireDesignation)
        {
            var classe = await _dbContext.Classe.FirstOrDefaultAsync(c=>c.Id == classeId);
            if(classe == null) return null;

            if(!string.IsNullOrEmpty(anneeScolaireDesignation))
            {
                var anneeScolaire = await _dbContext.AnneeScolaire
                                        .FirstOrDefaultAsync(a=>a.Designation == anneeScolaireDesignation);
                if(anneeScolaire == null) return null;

                var fraisConcernerClasses = _dbContext.FraisConcernerClasses.AsQueryable();
                fraisConcernerClasses = fraisConcernerClasses.Where(i=>i.ClasseId == classeId 
                                                && i.AnneeScolaireId == anneeScolaire.Id);
                return await fraisConcernerClasses.SumAsync(f=>f.Montant);
            }
            else
            {
                var anneeScolaireActive = await _dbContext.AnneeScolaire
                                    .FirstOrDefaultAsync(a=>a.EstActive == true);

                if(anneeScolaireActive == null) return null;

                var fraisConcernerClasses = _dbContext.FraisConcernerClasses.AsQueryable();
                fraisConcernerClasses = fraisConcernerClasses.Where(i=>i.ClasseId == classeId 
                                                && i.AnneeScolaireId == anneeScolaireActive.Id);
                return await fraisConcernerClasses.SumAsync(f=>f.Montant);
            }  
        }

        public Task<Classe?> GetMontantAPayerParClasseAsync(int classeId, string anneeScolaireDesignation)
        {
            throw new NotImplementedException();
        }

    }
}