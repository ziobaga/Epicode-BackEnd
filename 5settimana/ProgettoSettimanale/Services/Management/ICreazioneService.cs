using ProgettoSettimanale.Models;

namespace ProgettoSettimanale.Services.Management
{
    public interface ICreazioneService
    {
        Cliente CreazioneCliente(Cliente cliente);
        Camera CreazioneCamera(Camera camera);
        Prenotazione CreazionePrenotazione(Prenotazione prenotazione);
    }
}
