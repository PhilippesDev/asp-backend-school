namespace api_gestion_ecole.Dtos.Bulletins
{
    public class LigneBulletinDto
    {
        public int CoursId { get; set; }
        public string CoursDesignation { get; set; } = string.Empty;
        public string? CoursAbreviation { get; set; }
        public double NoteObtenue { get; set; }
        public double NoteMaximale { get; set; }
        public double PourcentageCours => NoteMaximale > 0 ? Math.Round((NoteObtenue / NoteMaximale) * 100, 2) : 0;
    }
}