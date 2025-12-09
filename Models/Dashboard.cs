namespace gestionCompte.Models
{
    public class Dashboard
    {
        public Compte Compte { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
        public Statistiques Statistiques { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
