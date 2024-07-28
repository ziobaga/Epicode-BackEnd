using ProgettoSettimanale.Models.Ricerca;

namespace ProgettoSettimanale.Services
{
    public interface ICheckoutService
    {
        public Task<CheckoutRicerca> PrenotazioneConImportoDaSaldare(int PrenotazioneId);
    }
}
