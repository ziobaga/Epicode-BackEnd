using Project.Models;
namespace Project.Services.Management
{
    public interface ICreazioneService
    {
        Persona CreaPersona(Persona persona);
        Camera CreaCamera(Camera camera);
        Prenotazione CreaPrenotazione(Prenotazione prenotazione);
    }
}
