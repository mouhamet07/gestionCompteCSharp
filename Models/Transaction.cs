namespace gestionCompte.Models
{
    public enum TransactionType
    {
        DEPOT,
        RETRAIT
    }
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }
        public decimal Montant { get; set; }
        public decimal SoldeApres { get; set; }
        public string Description { get; set; } 
        public int CompteId { get; set; }
        public Compte Compte { get; set; }
    }
}

