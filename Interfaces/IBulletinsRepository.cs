using api_gestion_ecole.Dtos.Bulletins;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Interfaces
{
    public interface IBulletinsRepository
    {
        Task<(Inscription? Inscription, List<CoursConcernerClasse> CoursClasse)> GetDonneesBulletinAsync(int inscriptionId, int periodeId);
        Task<(Inscription? Inscription, List<CoursConcernerClasse> CoursClasse, List<Periode> Periodes)> GetDonneesSemestreAsync(int inscriptionId, int semestreId);
        Task<Dictionary<int, double>> GetPointsClassePourSemestreAsync(int classeId, int anneeScolaireId, List<Periode> periodes);

        // À AJOUTER : Les deux nouvelles méthodes de calcul qui retournent le BulletinDto
        Task<BulletinDto?> CalculerBulletinPeriodeAsync(int inscriptionId, int periodeId);
        Task<BulletinDto?> CalculerBulletinSemestreAsync(int inscriptionId, int semestreId);
    }
}