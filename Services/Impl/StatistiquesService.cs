using gestionCompte.Models;
using gestionCompte.Data;
using gestionCompte.Controllers;
namespace gestionCompte.Services.Impl
{
    public class StatistiquesService : IStatistiquesService
    {
        private readonly GestionCompteContext _context ;
        private readonly ILogger<DashboardController> _logger;
        public StatistiquesService(GestionCompteContext context,ILogger<DashboardController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public Statistiques? GetStatistiques(int idCompte)
        {
            try
            {
                if (idCompte > 0)
                {
                    return _context.Statistiques.Where(s => s.IdCompte == idCompte).FirstOrDefault();
                }
                return null;
            }
            catch (System.Exception)
            {
                _logger.LogError("Erreur lors de la récupération des stats du compte");
                throw;
            }
        }
    }
}