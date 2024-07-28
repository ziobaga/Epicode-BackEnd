using Project.Models.ViewModels;

namespace Project.Services
{
    public interface ICheckoutService
    {
        public Task<CheckoutVM> GetPrenotazioneConImportoDaSaldare(int idPrenotazione);
    }
}
