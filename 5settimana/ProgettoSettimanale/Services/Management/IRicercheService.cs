using Project.Models;

namespace Project.Services.Management
{
    public interface IRicercheService
    {
        public Task<List<Prenotazione>> GetPrenotazioniCFAsync(string codiceFiscale);
        public Task<List<Prenotazione>> GetPrenotazioniTipoPensioneAsync(string tipoPensione);
    }
}
