namespace gestionCompte.Models
{
    public enum CompteType
    {
        COURANT,
        EPARGNE
    }
    public class Compte
    {
        public int Id { get; set; }
        public string Numero { get; set; } 
        public CompteType Type { get; set; } 
        public decimal SoldeActuel { get; set; }
        public DateTime DateCreation { get; set; }
        public Boolean Statut { get; set; } = true;
        public DateTime? DateDeblocage { get; set; }
        public int TitulaireId { get; set; }
        public Titulaire Titulaire { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}