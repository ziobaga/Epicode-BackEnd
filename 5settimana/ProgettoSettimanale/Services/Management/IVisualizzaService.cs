using ProgettoSettimanale.Models;

namespace ProgettoSettimanale.Services.Management
{
    public interface IVisualizzaService
    {
        List<Camera> GetAllCamere(); 
        List<Cliente> GetAllClienti(); 
        List<Prenotazione> GetAllPrenotazioni();
    }
}
