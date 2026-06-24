namespace api_gestion_ecole.Dtos.Bulletins
{
    public class BulletinDto
    {
        public int InscriptionId { get; set; }
        public string EleveNomComplet { get; set; } = string.Empty;
        public string Sexe { get; set; } = string.Empty;
        public string ClasseDesignation { get; set; } = string.Empty;
        public string AnneeScolaireDesignation { get; set; } = string.Empty;

        public string LibellePeriode { get; set; } = string.Empty; 

        public int Rang { get; set; }
        public int EffectifClasse { get; set; }
        public List<LigneBulletinDto> Lignes { get; set; } = new();
        public double TotalObtenu { get; set; }
        public double TotalMaximum { get; set; }
        public double PourcentageGeneral => TotalMaximum > 0 ? Math.Round((TotalObtenu / TotalMaximum) * 100, 2) : 0;
    }
}