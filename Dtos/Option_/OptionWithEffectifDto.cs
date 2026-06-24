namespace api_gestion_ecole.Dtos.Option_
{
    public class OptionWithEffectifDto
    {
        public int Id { get; set; }
        public string Designation { get; set; } = "";
        public string? Abreviation { get; set; }
        public int NombreClasses { get; set; }
        public int Effectif { get; set; }
    }
}
