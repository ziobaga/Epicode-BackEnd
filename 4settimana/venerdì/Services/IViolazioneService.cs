using venerdì.Models;

namespace venerdì.Services
{
    public interface IViolazioneService
    {
        TipoViolazione Create(TipoViolazione violazione);  
        List<TipoViolazione> GetAllViolazioni();  
        List<ViolazioneSopra10Punti> GetViolazioneOver10Punti();  
        List<ViolazioneSopra400Euro> GetViolazioneOver400Euro();   
    }
}
