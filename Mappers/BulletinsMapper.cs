using api_gestion_ecole.Dtos.Bulletins;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Mappers
{
   public static class BulletinMapper
    {
        // public static BulletinDto ToBulletinCompletDto(Inscription inscription, List<CoursConcernerClasse> coursClasse, Periode periode)
        // {
        //     var dto = new BulletinDto
        //     {
        //         InscriptionId = inscription.Id,
        //         EleveNomComplet = $"{inscription.Eleve?.Nom} {inscription.Eleve?.Postnom} {inscription.Eleve?.Prenom}".Trim(),
        //         Sexe = inscription.Eleve?.Sexe ?? string.Empty,
        //         ClasseDesignation = inscription.Classe?.Designation ?? string.Empty,
        //         AnneeScolaireDesignation = inscription.AnneeScolaire?.Designation ?? string.Empty,
        //         // PeriodeDesignation = periode.Designation
        //     };

        //     double cumulObtenu = 0;
        //     double cumulMaximum = 0;

        //     foreach (var cc in coursClasse)
        //     {
        //         if (cc.Cours == null) continue;

                
        //         var cotationEleve = inscription.Cotations?
        //             .FirstOrDefault(c => c.CoursConcernerClasseId == cc.Id);

                
        //         double noteCalculee = cotationEleve != null ? (cotationEleve.Cote * periode.Coefficient) : 0;
        //         double maxCalcule = cc.Max * periode.Coefficient;

        //         dto.Lignes.Add(new LigneBulletinDto
        //         {
        //             CoursId = cc.Cours.Id,
        //             CoursDesignation = cc.Cours.Designation,
        //             CoursAbreviation = cc.Cours.Abreviation,
        //             NoteObtenue = Math.Round(noteCalculee, 2),
        //             NoteMaximale = Math.Round(maxCalcule, 2)
        //         });

        //         cumulObtenu += noteCalculee;
        //         cumulMaximum += maxCalcule;
        //     }

        //     dto.TotalObtenu = Math.Round(cumulObtenu, 2);
        //     dto.TotalMaximum = Math.Round(cumulMaximum, 2);

        //     return dto;
        // }

         public static BulletinDto ToBulletinDto(
            Inscription inscription, 
            List<CoursConcernerClasse> coursClasse, 
            List<Periode> periodesEvaluees, 
            Dictionary<int, double> pointsTouteLaClasse,
            string titreBulletin)
        {
            var dto = new BulletinDto
            {
                InscriptionId = inscription.Id,
                EleveNomComplet = $"{inscription.Eleve?.Nom} {inscription.Eleve?.Postnom} {inscription.Eleve?.Prenom}".Trim(),
                Sexe = inscription.Eleve?.Sexe ?? string.Empty,
                ClasseDesignation = inscription.Classe?.Designation ?? string.Empty,
                AnneeScolaireDesignation = inscription.AnneeScolaire?.Designation ?? string.Empty,
                LibellePeriode = titreBulletin, 
                EffectifClasse = pointsTouteLaClasse.Count
            };

            // 1. CALCUL DU RANG (Tri décroissant des totaux de toute la classe)
            var classement = pointsTouteLaClasse
                .OrderByDescending(x => x.Value)
                .Select((x, index) => new { InscriptionId = x.Key, Position = index + 1 })
                .ToList();

            var rangEleve = classement.FirstOrDefault(x => x.InscriptionId == inscription.Id);
            dto.Rang = rangEleve?.Position ?? 0;

            // 2. CALCUL DES NOTES PAR MATIÈRE
            double cumulGeneralObtenu = 0;
            double cumulGeneralMaximum = 0;

            // On boucle sur TOUS les cours de la classe (garantit l'affichage même si l'élève n'a pas de note)
            foreach (var cc in coursClasse)
            {
                if (cc.Cours == null) continue;

                double noteMatiereCumulee = 0;
                double maxMatiereCumule = 0;

                // On accumule les points pour chaque période évaluée (ex: si Semestre, on fait P1 + P2 + Examen)
                foreach (var per in periodesEvaluees)
                {
                    // On cherche si l'élève a une note pour ce cours et pour cette période spécifique
                    var cotationPeriode = inscription.Cotations?
                        .FirstOrDefault(c => c.CoursConcernerClasseId == cc.Id && c.PeriodeId == per.Id);

                    // Si l'élève n'a pas été coté (absence ou oubli), la note brute vaut 0
                    double noteBrute = cotationPeriode != null ? cotationPeriode.Cote : 0;

                    // Application du coefficient (ex: 1.0 pour P1, 2.0 pour l'Examen)
                    noteMatiereCumulee += (noteBrute * per.Coefficient);
                    maxMatiereCumule += (cc.Max * per.Coefficient);
                }

                // Ajout de la ligne de la matière dans le bulletin
                dto.Lignes.Add(new LigneBulletinDto
                {
                    CoursId = cc.Cours.Id,
                    CoursDesignation = cc.Cours.Designation,
                    CoursAbreviation = cc.Cours.Abreviation,
                    NoteObtenue = Math.Round(noteMatiereCumulee, 2),
                    NoteMaximale = Math.Round(maxMatiereCumule, 2)
                });

                // Cumul pour le total général du bulletin
                cumulGeneralObtenu += noteMatiereCumulee;
                cumulGeneralMaximum += maxMatiereCumule;
            }

            // Enregistrement des totaux généraux
            dto.TotalObtenu = Math.Round(cumulGeneralObtenu, 2);
            dto.TotalMaximum = Math.Round(cumulGeneralMaximum, 2);

            return dto;
        }    } 
}


