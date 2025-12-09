using gestionCompte.Models;
using gestionCompte.Data;
using gestionCompte.Controllers;
namespace gestionCompte.Services.Impl
{
    public class TransactionService : ITransactionService
    {
        private const int size = 10; 
        private readonly GestionCompteContext _context ;
        private readonly ILogger<DashboardController> _logger;
        public TransactionService(GestionCompteContext context,ILogger<DashboardController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IEnumerable<Transaction> GetTransactions(int page, int idCompte, TransactionType? type=null )
        {
            try
            {
                if(page < 1) page = 1;
            int offset = (page - 1) * size;
            var transactions = _context.Transactions.Where(t => t.CompteId == idCompte );
            if (type != null)
            {
                transactions = transactions.Where(t => t.Type == type);
            }
            return transactions
                    .OrderByDescending(t => t.Date)
                    .Skip(offset)
                    .Take(size)
                    .ToList();
            }
            catch (System.Exception)
            {
                _logger.LogError("Erreur lors de la récupération des Transactions - Page {Page}", page);
                throw;
            }
        }
        public int Count(int idCompte, TransactionType? type=null )
        {
            try
            {
                var transactions = _context.Transactions.Where(t => t.CompteId == idCompte );
                if (type != null)
                {
                    transactions = transactions.Where(t => t.Type == type);
                }
                return transactions
                        .OrderByDescending(t => t.Date)
                        .Count();
                }
                catch (System.Exception)
                {
                    _logger.LogError("Erreur lors du compte des Transactions");
                    throw;
                }
        }
    }
}