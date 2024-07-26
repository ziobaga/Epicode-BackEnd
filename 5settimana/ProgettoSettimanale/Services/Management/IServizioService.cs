using ProgettoSettimanale.Models;

namespace ProgettoSettimanale.Services.Management
{
    public interface IServizioService
    {

        public PrenotazioneServizi AddServizioAgg(PrenotazioneServizi prenotazioneServizioAgg, int IdPrenotazione);
        public List<Servizio> GetServiziAgg();
    }
}
