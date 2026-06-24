using Microsoft.VisualBasic;

namespace api_gestion_ecole.Dtos.Parent
{
    public class ParentDto
    {
        public int Id { get; set; }
        public string Nom { get; set; } = "";
        public string Postnom { get; set; } = "";
        public string Prenom { get; set; } = "";
        public string Sexe { get; set; } = "";
        public string Profession { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
    }
}