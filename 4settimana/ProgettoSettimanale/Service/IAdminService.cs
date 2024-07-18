using ProgettoSettimanale.Models;

namespace ProgettoSettimanale.Service
{
    public interface IAdminService
    {
        public List<Spedizione> SpedizioniInConsegnaOggi();
        public int TotSpedizioniNonConsegnate();
        public List<SpedizionePerCittaResult> SpedizioniPerCitta();
        public List<Spedizione> GetAllSpedizioni();
        public List<ClienteAzienda> GetAllAzienda();
        public List<ClientePrivato> GetAllPrivato();
    }
}
