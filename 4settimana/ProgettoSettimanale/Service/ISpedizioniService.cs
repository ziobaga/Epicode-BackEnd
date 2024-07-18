using ProgettoSettimanale.Models;

namespace ProgettoSettimanale.Service
{
    public interface ISpedizioniService
    {
        List<Spedizione> SpedizioniPerClientePrivato(string codiceFiscale);
        List<Spedizione> SpedizioniPerClienteAzienda(string partitaIVA);
    }
}
