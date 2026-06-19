using api_gestion_ecole.Dtos.Paiement;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Interfaces
{
    public interface IPaiementRepository
    {
         public Task<List<Paiement>> GetAllAsync();
        public Task<Paiement?> GetByIdAsync(int id);
        public Task<Paiement?> CreateAsync(CreatePaiementDto  createPaiementDto);
        public Task<Paiement?> UpdateAsync(int id, UpdatePaiementDto updatePaiementDto);
        public Task<Paiement?> DeleteAsync(int id);
        public Task<bool> IsInscriptionExistAsync(int id);
        public Task<bool> IsFraisConcernerClasseExistAsync(int id);
        // public Task<bool> IsAnneeScolaireExistAsync(int id);
    }
}