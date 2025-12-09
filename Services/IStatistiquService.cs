using gestionCompte.Models;

namespace gestionCompte.Services
{
    public interface IStatistiquesService
    {
        Statistiques? GetStatistiques(int idCompte);
    }
}