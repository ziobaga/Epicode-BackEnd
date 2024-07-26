using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgettoSettimanale.Models;
using ProgettoSettimanale.Models.Ricerca;
using ProgettoSettimanale.Services.Management;

namespace ProgettoSettimanale.Controllers.Management
{
    [Authorize]
    public class ServizioController : Controller
    {
        private readonly IServizioService _servizioService;

        public ServizioController(IServizioService servizioService)
        {
            _servizioService = servizioService;
        }

        // Azione per visualizzare l'elenco dei servizi aggiuntivi disponibili
        public IActionResult Index()
        {
            var servizi = _servizioService.GetServiziAgg();
            return View(servizi);
        }

        // Azione GET per aggiungere un servizio aggiuntivo a una prenotazione
        public IActionResult AddServizio(int prenotazioneId)
        {
            ViewBag.Servizi = _servizioService.GetServiziAgg();
            var viewModel = new AddServizioRicerca
            {
                PrenotazioneId = prenotazioneId
            };
            return View(viewModel);
        }

        // Azione POST per aggiungere un servizio aggiuntivo a una prenotazione
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddServizio(AddServizioRicerca model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var servizio = new PrenotazioneServizi
                    {
                        PrenotazioneId = model.PrenotazioneId,
                        ServizioId = model.ServizioId,
                        Data = model.Data,
                        Quantita = model.Quantita,
                        Prezzo = model.Prezzo
                    };

                    _servizioService.AddServizioAgg(servizio, model.PrenotazioneId);
                    return RedirectToAction("TotPrenotazioni", "Visualizza", new { id = model.PrenotazioneId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Errore durante l'aggiunta del servizio aggiuntivo.");
                }
            }

            ViewBag.Servizi = _servizioService.GetServiziAgg();
            return View(model);
        }
    }
}
