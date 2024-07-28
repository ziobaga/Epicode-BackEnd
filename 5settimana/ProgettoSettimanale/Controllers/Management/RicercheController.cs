using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Models.ViewModels;
using Project.Services.Management;


namespace Project.Controllers
{
    [Authorize]
    [Route("Management/[controller]")]
    public class RicercheController : Controller
    {
        private readonly IRicercheService _ricercheService;

        public RicercheController(IRicercheService ricercheService)
        {
            _ricercheService = ricercheService;
        }

        [HttpGet("RicercaCF")]
        public IActionResult RicercaCF()
        {
            return View();
        }

        [HttpPost("RicercaCF")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RicercaCF(string codiceFiscale)
        {
            if (string.IsNullOrWhiteSpace(codiceFiscale))
            {
                ModelState.AddModelError("", "Il codice fiscale non può essere nullo o vuoto.");
                return View();
            }

            try
            {
                var prenotazioni = await _ricercheService.GetPrenotazioniCFAsync(codiceFiscale);

                if (prenotazioni != null && prenotazioni.Any())
                {
                    return RedirectToAction("RisultatiCF", new { codiceFiscale });
                }
                else
                {
                    ModelState.AddModelError("", "Nessuna prenotazione trovata per il codice fiscale fornito.");
                    return View();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Errore interno del server. Si prega di riprovare più tardi.");
            }
        }

        [HttpGet("RisultatiCF")]
        public async Task<IActionResult> RisultatiCF(string codiceFiscale)
        {
            if (string.IsNullOrWhiteSpace(codiceFiscale))
            {
                return RedirectToAction("RicercaCF");
            }

            try
            {
                var prenotazioni = await _ricercheService.GetPrenotazioniCFAsync(codiceFiscale);

                if (prenotazioni == null || !prenotazioni.Any())
                {
                    ViewBag.Message = "Nessuna prenotazione trovata per il codice fiscale fornito.";
                }

                return View(prenotazioni);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Errore interno del server. Si prega di riprovare più tardi.");
            }
        }

        [HttpGet("RicercaTipoPensione")]
        public IActionResult RicercaTipoPensione()
        {
            return View();
        }

        [HttpPost("RicercaTipoPensione")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RicercaTipoPensione(RicercaPrenotazioneTipoPensioneVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var prenotazioni = await _ricercheService.GetPrenotazioniTipoPensioneAsync(model.TipoPensione);

                if (prenotazioni != null && prenotazioni.Any())
                {
                    return RedirectToAction("RisultatiTipoPensione", new { tipoPensione = model.TipoPensione });
                }
                else
                {
                    ModelState.AddModelError("", "Nessuna prenotazione trovata per il tipo di pensione selezionato.");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Errore interno del server. Si prega di riprovare più tardi.");
            }
        }


        [HttpGet("RisultatiTipoPensione")]
        public async Task<IActionResult> RisultatiTipoPensione(string tipoPensione)
        {
            if (string.IsNullOrWhiteSpace(tipoPensione))
            {
                return RedirectToAction("RicercaTipoPensione");
            }

            try
            {
                var prenotazioni = await _ricercheService.GetPrenotazioniTipoPensioneAsync(tipoPensione);

                if (prenotazioni == null || !prenotazioni.Any())
                {
                    ViewBag.Message = "Nessuna prenotazione trovata per il tipo di pensione selezionato.";
                }

                return View(prenotazioni);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Errore interno del server. Si prega di riprovare più tardi.");
            }
        }

    }

}
