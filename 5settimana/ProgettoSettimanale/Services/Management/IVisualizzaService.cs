using Project.Models;

namespace Project.Services.Management
{
    public interface IVisualizzaService
    {
        List<Camera> GetAllCamere();
        List<Persona> GetAllPersone();
        List<Prenotazione> GetAllPrenotazioni();
    }
}
