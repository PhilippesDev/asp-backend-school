namespace api_gestion_ecole.Dtos.Periode
{
    public class PeriodeDto
    {
        public int Id { get; set; }
        public int SemestreId { get; set; }
        public string Designation { get; set; } = string.Empty;
        public double Coefficient { get; set; }
        public string Semestre { get; set; } = string.Empty;
    }
}