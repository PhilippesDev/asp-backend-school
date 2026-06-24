using api_gestion_ecole.Data;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Models;
using Microsoft.EntityFrameworkCore;
using api_gestion_ecole.Dtos.Bulletins;

namespace api_gestion_ecole.Repositories
{
    public class BulletinsRepository: IBulletinsRepository
    {
        private readonly ApplicationDbContext _context;
        public BulletinsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(Inscription? Inscription, List<CoursConcernerClasse> CoursClasse)> GetDonneesBulletinAsync(int inscriptionId, int periodeId)
        {
            var inscription = await _context.Inscription
                .Include(i => i.Eleve)
                .Include(i => i.AnneeScolaire)
                .Include(i => i.Classe)
                .Include(i => i.Cotations!.Where(c => c.PeriodeId == periodeId))
                .FirstOrDefaultAsync(i => i.Id == inscriptionId);

            if (inscription == null) return (null, new List<CoursConcernerClasse>());

            var coursClasse = await _context.CoursConcernerClasse
                .Include(ccc => ccc.Cours)
                .Where(ccc => ccc.ClasseId == inscription.ClasseId && ccc.AnneeScolaireId == inscription.AnneeScolaireId)
                .ToListAsync();

            return (inscription, coursClasse);
        }

        public async Task<(Inscription? Inscription, List<CoursConcernerClasse> CoursClasse, List<Periode> Periodes)> GetDonneesSemestreAsync(int inscriptionId, int semestreId)
        {
            var periodes = await _context.Periode
                .Where(p => p.SemestreId == semestreId)
                .ToListAsync();

            var periodeIds = periodes.Select(p => p.Id).ToList();

            var inscription = await _context.Inscription
                .Include(i => i.Eleve)
                .Include(i => i.AnneeScolaire)
                .Include(i => i.Classe)
                .Include(i => i.Cotations!.Where(c => periodeIds.Contains(c.PeriodeId)))
                .FirstOrDefaultAsync(i => i.Id == inscriptionId);

            if (inscription == null) return (null, new List<CoursConcernerClasse>(), periodes);

           
            var coursClasse = await _context.CoursConcernerClasse
                .Include(ccc => ccc.Cours)
                .Where(ccc => ccc.ClasseId == inscription.ClasseId && ccc.AnneeScolaireId == inscription.AnneeScolaireId)
                .ToListAsync();

            return (inscription, coursClasse, periodes);
        }

        public async Task<Dictionary<int, double>> GetPointsClassePourSemestreAsync(int classeId, int anneeScolaireId, List<Periode> periodes)
        {
            var periodeIds = periodes.Select(p => p.Id).ToList();

            var inscriptions = await _context.Inscription
                .Where(i => i.ClasseId == classeId && i.AnneeScolaireId == anneeScolaireId)
                .Include(i => i.Cotations)
                .ToListAsync();

            var classementPoints = new Dictionary<int, double>();

            foreach (var ins in inscriptions)
            {
                double totalEleve = 0;

                if (ins.Cotations != null)
                {
                    var cotationsSemestre = ins.Cotations.Where(c => periodeIds.Contains(c.PeriodeId));
                    foreach (var cotation in cotationsSemestre)
                    {
                        var per = periodes.FirstOrDefault(p => p.Id == cotation.PeriodeId);
                        double coeff = per?.Coefficient ?? 1.0;
                        totalEleve += (cotation.Cote * coeff);
                    }
                }

                classementPoints.Add(ins.Id, totalEleve);
            }

            return classementPoints;
        }

        public async Task<BulletinDto?> CalculerBulletinPeriodeAsync(int inscriptionId, int periodeId)
        {
            // 1. Récupérer les données brutes grâce à ta méthode
            var (inscription, coursClasse) = await GetDonneesBulletinAsync(inscriptionId, periodeId);
            if (inscription == null) return null;

            var periode = await _context.Periode.FindAsync(periodeId);
            if (periode == null) return null;

            // 2. Calculer l'effectif de la classe
            var effectif = await _context.Inscription
                .CountAsync(i => i.ClasseId == inscription.ClasseId && i.AnneeScolaireId == inscription.AnneeScolaireId);

            // 3. Récupérer le libellé de la période
            var bulletin = new BulletinDto
            {
                InscriptionId = inscription.Id,
                EleveNomComplet = $"{inscription.Eleve?.Nom} {inscription.Eleve?.Postnom} {inscription.Eleve?.Prenom}".Trim(),
                Sexe = inscription.Eleve?.Sexe ?? "",
                ClasseDesignation = inscription.Classe?.Designation ?? "",
                AnneeScolaireDesignation = inscription.AnneeScolaire?.Designation ?? "",
                LibellePeriode = periode.Designation,
                EffectifClasse = effectif
            };

            double totalObtenu = 0;
            double totalMaximum = 0;

            // 4. Construire les lignes de cours (Règle du 0 automatique si pas de note)
            foreach (var cc in coursClasse)
            {
                // On cherche la cotation pour ce cours précis
                var cotation = inscription.Cotations?.FirstOrDefault(c => c.CoursConcernerClasseId == cc.Id);
                
                double noteObtenue = cotation?.Cote ?? 0.0; // Si pas coté, on affecte 0
                double maxPeriode = cc.Max * periode.Coefficient;
                double notePonderee = noteObtenue * periode.Coefficient;

                bulletin.Lignes.Add(new LigneBulletinDto
                {
                    CoursId = cc.CoursId,
                    CoursDesignation = cc.Cours?.Designation ?? "Cours inconnu",
                    CoursAbreviation = cc.Cours?.Abreviation,
                    NoteObtenue = notePonderee,
                    NoteMaximale = maxPeriode
                });

                totalObtenu += notePonderee;
                totalMaximum += maxPeriode;
            }

            bulletin.TotalObtenu = totalObtenu;
            bulletin.TotalMaximum = totalMaximum;

            // 5. Calcul du Rang pour la période
            bulletin.Rang = await CalculerRangPeriodeAsync(inscription.ClasseId, inscription.AnneeScolaireId, periode, totalObtenu);

            return bulletin;
        }

        // Méthode d'aide pour le rang de la période
        private async Task<int> CalculerRangPeriodeAsync(int classeId, int anneeScolaireId, Periode periode, double totalEleveActuel)
        {
            var inscriptions = await _context.Inscription
                .Where(i => i.ClasseId == classeId && i.AnneeScolaireId == anneeScolaireId)
                .Include(i => i.Cotations)
                .ToListAsync();

            int rang = 1;
            foreach (var ins in inscriptions)
            {
                if (ins.Id == ins.Id) continue; // Ignorer l'élève lui-même lors de la boucle si nécessaire, mais plus simple de comparer les totaux :
                
                double totalAutre = 0;
                var cotation = ins.Cotations?.FirstOrDefault(c => c.PeriodeId == periode.Id);
                if (cotation != null)
                {
                    totalAutre = cotation.Cote * periode.Coefficient;
                }

                if (totalAutre > totalEleveActuel)
                {
                    rang++;
                }
            }
            return rang;
        }

        public async Task<BulletinDto?> CalculerBulletinSemestreAsync(int inscriptionId, int semestreId)
        {
            
            var (inscription, coursClasse, periodes) = await GetDonneesSemestreAsync(inscriptionId, semestreId);
            if (inscription == null) return null;

            var semestre = await _context.Semestre.FindAsync(semestreId);
            var designationSemestre = semestre?.Designation ?? "Semestre";

            // 2. Calculer les totaux de la classe pour le Rang
            var classement = await GetPointsClassePourSemestreAsync(inscription.ClasseId, inscription.AnneeScolaireId, periodes);
            
            // Calcul de la note totale de l'élève actuel pour le semestre
            double totalObtenuSemestre = classement.ContainsKey(inscriptionId) ? classement[inscriptionId] : 0;

            // Calcul du Rang (Combien de personnes ont plus de points que lui ?)
            int rang = classement.Values.Count(points => points > totalObtenuSemestre) + 1;

            var bulletin = new BulletinDto
            {
                InscriptionId = inscription.Id,
                EleveNomComplet = $"{inscription.Eleve?.Nom} {inscription.Eleve?.Postnom} {inscription.Eleve?.Prenom}".Trim(),
                Sexe = inscription.Eleve?.Sexe ?? "",
                ClasseDesignation = inscription.Classe?.Designation ?? "",
                AnneeScolaireDesignation = inscription.AnneeScolaire?.Designation ?? "",
                LibellePeriode = designationSemestre,
                EffectifClasse = classement.Count,
                Rang = rang
            };

            double totalMaximumSemestre = 0;

            // 3. Grouper les notes par cours pour tout le semestre
            foreach (var cc in coursClasse)
            {
                double noteCoursCumulee = 0;
                double maxCoursCumule = 0;

                // On passe sur chaque période du semestre (P1, P2, Examen...)
                foreach (var per in periodes)
                {
                    var cotation = inscription.Cotations?
                        .FirstOrDefault(c => c.CoursConcernerClasseId == cc.Id && c.PeriodeId == per.Id);

                    double coteBrute = cotation?.Cote ?? 0.0; // Règle du 0 automatique
                    
                    noteCoursCumulee += (coteBrute * per.Coefficient);
                    maxCoursCumule += (cc.Max * per.Coefficient);
                }

                bulletin.Lignes.Add(new LigneBulletinDto
                {
                    CoursId = cc.CoursId,
                    CoursDesignation = cc.Cours?.Designation ?? "Cours inconnu",
                    CoursAbreviation = cc.Cours?.Abreviation,
                    NoteObtenue = noteCoursCumulee,
                    NoteMaximale = maxCoursCumule
                });

                totalMaximumSemestre += maxCoursCumule;
            }

            bulletin.TotalObtenu = totalObtenuSemestre;
            bulletin.TotalMaximum = totalMaximumSemestre;

            return bulletin;
        }
    }
}