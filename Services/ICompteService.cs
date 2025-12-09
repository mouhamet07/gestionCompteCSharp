using gestionCompte.Models;

namespace gestionCompte.Services
{
    public interface ICompteService
    {
        Compte? GetAccountByNum(String numero);
    }
}