using api_gestion_ecole.Data;
using api_gestion_ecole.Models;
using Microsoft.EntityFrameworkCore;

namespace api_gestion_ecole.Helpers
{
    public static class AnneeScolaireResolver
    {
        public static async Task<AnneeScolaire?> ResolveAsync(
            ApplicationDbContext dbContext, string anneeScolaireDesignation)
        {
            if (!string.IsNullOrEmpty(anneeScolaireDesignation))
                return await dbContext.AnneeScolaire
                    .FirstOrDefaultAsync(a => a.Designation == anneeScolaireDesignation);

            return await dbContext.AnneeScolaire
                .FirstOrDefaultAsync(a => a.EstActive == true);
        }

        public static async Task<AnneeScolaire?> ResolveByIdAsync(
            ApplicationDbContext dbContext, int anneeScolaireId)
        {
            return await dbContext.AnneeScolaire
                .FirstOrDefaultAsync(a => a.Id == anneeScolaireId);
        }
    }
}
