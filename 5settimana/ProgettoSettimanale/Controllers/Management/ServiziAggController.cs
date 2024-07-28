using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Models.ViewModels;
using Project.Services.Management;

namespace Project.Controllers.Management
{
    [Authorize]
    public class ServiziAggController : Controller
    {
        private readonly IAddServizi _addServiziAggService;

        public ServiziAggController(IAddServizi addServiziAggService)
        {
            _addServiziAggService = addServiziAggService;
        }

        public IActionResult AddServizio(int idPrenotazione)
        {
            ViewBag.serviziAgg = _addServiziAggService.GetServizi();
            var viewModel = new AddServizioAggVM
            {
                IdPrenotazione = idPrenotazione
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddServizio(AddServizioAggVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var servizioAgg = new PrenotazioneServizioAgg
                    {
                        IdPrenotazione = model.IdPrenotazione,
                        IdServizioAgg = model.IdServizioAgg,
                        Data = model.Data,
                        Quantita = model.Quantita,
                        Prezzo = model.Prezzo
                    };

                    _addServiziAggService.AddServizio(servizioAgg, model.IdPrenotazione);
                    return RedirectToAction("ListaPrenotazioni", "Visualizzazioni");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Errore durante l'aggiunta del servizio aggiuntivo.");

                }
            }

            return View("AddServiziAgg", model);
        }
    }
}
