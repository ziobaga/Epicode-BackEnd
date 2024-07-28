using Project.Models;

namespace Project.Services.Management
{
    public interface IAddServizi
    {
        public PrenotazioneServizioAgg AddServizio(PrenotazioneServizioAgg prenotazioneServizioAgg, int IdPrenotazione);
        public List<ServizioAgg> GetServizi();
    }
}
