using api_gestion_ecole.Dtos.Insciption;
using api_gestion_ecole.Helpers;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Interfaces
{
    public interface IInscriptionRepository
    {
        public Task<List<Inscription>> GetAllAsync(QueryObject queryObject);
        public Task<Inscription?> GetByIdAsync(int id);
        public Task<Inscription?> CreateAsync(CreateInscriptionDto createOptionDto);
        public Task<Inscription?> UpdateAsync(int inscriptionId,UpdateInscriptionDto updateInscriptionDto);
        public Task<Inscription?> DeleteAsync(int id);
        public Task<bool> IsEleveExistAsync(int id);
        public Task<bool> IsClasseExistAsync(int id);
        public Task<bool> IsAnneeScolaireExistAsync(int id);
        public Task<bool> IsEleveAlreadyInscritAsync(int EleveId, int AnneeScolaireId);
        public Task<int?> GetNombreInscriptionsAsync(string anneeScolaireDesignation);
    }
}