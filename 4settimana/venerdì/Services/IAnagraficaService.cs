using venerdì.Models;

namespace venerdì.Services
{
    public interface IAnagraficaService
    {

        Anagrafica Create(Anagrafica anagrafica);
        List<PuntiDecurtati> GetAllTrasgressoreByPuntiDecurtati();
    }
}
