using gestionCompte.Models;
using gestionCompte.Data;
using gestionCompte.Controllers;
namespace gestionCompte.Services.Impl
{
    public class CompteService : ICompteService
    {
        private readonly GestionCompteContext _context ;
        private readonly ILogger<DashboardController> _logger;
        public CompteService(GestionCompteContext context,ILogger<DashboardController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public Compte? GetAccountByNum(String num)
        {
            try
            {
                var compte = _context.Comptes.Where(c => c.Statut);
                if (num != null)
                {
                    return compte.Where(c => c.Numero == num).FirstOrDefault();
                }
                return null;
            }
            catch (System.Exception)
            {
                _logger.LogError("Erreur lors de la récupération du Compte");
                throw;
            }
        }
    }
}