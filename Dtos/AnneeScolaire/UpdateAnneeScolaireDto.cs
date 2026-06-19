namespace api_gestion_ecole.Dtos.AnneeScolaire
{
    public class UpdateAnneeScolaireDto : CreateAnneeScolaireDto
    {
        public bool EstActive { get; set; }
    }
}