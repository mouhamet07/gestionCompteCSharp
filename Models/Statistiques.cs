
namespace gestionCompte.Models
{
    public class Statistiques
    {
        public int Id { get; set; }
        public decimal TotalDepots { get; set; }
        public decimal TotalRetraits { get; set; }
        public int NombreTransactions { get; set; }
        public DateTime? DerniereTransaction { get; set; }
        public int IdCompte { get; set; }
    }
}
