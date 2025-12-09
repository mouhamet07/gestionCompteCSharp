using gestionCompte.Models;

namespace gestionCompte.Services
{
    public interface ITransactionService
    {
        IEnumerable<Transaction> GetTransactions(int page, int idCompte,TransactionType? type=null );
        int Count(int idCompte,TransactionType? type=null );
    }
}