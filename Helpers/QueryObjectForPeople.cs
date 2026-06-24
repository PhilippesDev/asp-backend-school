namespace api_gestion_ecole.Helpers
{
    public class QueryObjectForPeople
    {
        public string? Noms { get; set; }
        public int Page {get;set;} = 1;
        public int PageSize { get; set; } = 10;
        public bool IsDescending { get; set; }
    }
}