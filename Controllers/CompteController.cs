using Microsoft.AspNetCore.Mvc;
using gestionCompte.Models;
using gestionCompte.Services;

namespace gestionCompte.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly ICompteService _cs;
        private readonly ITransactionService _ts;
        private readonly IStatistiquesService _ss;
        public DashboardController(ILogger<DashboardController> logger,IStatistiquesService ss,ITransactionService ts, ICompteService cs)
        {
            _logger = logger;
            _cs = cs;
            _ss = ss;
            _ts = ts;
        }
        public IActionResult Index(int page = 1, string numCompte = "", TransactionType? type = null)
        {
            try
            {
                var compte = _cs.GetAccountByNum(numCompte);
                if (compte == null)
                {
                    return View(new Dashboard());
                }
                int total = _ts.Count(compte.Id, type);
                int nbrPages = (int)Math.Ceiling((double)total / 10);
                var transactions = _ts.GetTransactions(page, compte.Id, type);
                var stat = _ss.GetStatistiques(compte.Id);
                var dash = new Dashboard
                {
                    Compte = compte,
                    Transactions = transactions,
                    Statistiques = stat,
                    CurrentPage = page,
                    TotalPages = nbrPages
                };
                return View(dash);
            }
            catch (Exception)
            {
                _logger.LogError("Erreur lors de l'affichage du compte");
                return View(new Dashboard());
            }
        }
    }
}
