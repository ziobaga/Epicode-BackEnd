using ProgettoSettimanale.Models;

namespace ProgettoSettimanale.Services.Management
{
    public interface IRicercaService
    {
        public Task<List<Prenotazione>> GetPrenotazioniByCFAsync(string codiceFiscale);
        public Task<List<Prenotazione>> GetPrenotazioniByDettagliSoggiornoAsync(string dettagliSoggiorno);
    }
}
