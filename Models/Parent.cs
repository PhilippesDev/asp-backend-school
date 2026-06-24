namespace api_gestion_ecole.Models
{
    public class Parent
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Postnom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public string Sexe { get; set; } = string.Empty;
        public string Profession { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public IEnumerable<Eleve>? Eleves { get; set; }
    }
}