using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgettoSettimanale.Models.Ricerca;
using ProgettoSettimanale.Services.Management;

namespace ProgettoSettimanale.Controllers.Management
{
    [Authorize]
    [Route("Management/[controller]")]
    public class RicercaController : Controller
    {
        private readonly IRicercaService _ricercheService;

        public RicercaController(IRicercaService ricercheService)
        {
            _ricercheService = ricercheService;
        }

        [HttpGet("RicercaByCF")]
        public IActionResult RicercaByCF()
        {
            return View();
        }

        [HttpPost("RicercaByCF")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RicercaByCF(string codiceFiscale)
        {
            if (string.IsNullOrWhiteSpace(codiceFiscale))
            {
                ModelState.AddModelError("", "Il codice fiscale non può essere nullo o vuoto.");
                return View();
            }

            try
            {
                var prenotazioni = await _ricercheService.GetPrenotazioniByCFAsync(codiceFiscale);

                if (prenotazioni != null && prenotazioni.Any())
                {
                    return RedirectToAction("TotByCF", new { codiceFiscale });
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

        [HttpGet("TotByCF")]
        public async Task<IActionResult> TotByCF(string codiceFiscale)
        {
            if (string.IsNullOrWhiteSpace(codiceFiscale))
            {
                return RedirectToAction("RicercaByCF");
            }

            try
            {
                var prenotazioni = await _ricercheService.GetPrenotazioniByCFAsync(codiceFiscale);

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

        [HttpGet("RicercaByDettagliSoggiorno")]
        public IActionResult RicercaByDettagliSoggiorno()
        {
            return View();
        }

        [HttpPost("RicercaByDettagliSoggiorno")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RicercaByDettagliSoggiorno(RicercaPrenotazioneByDettagliSoggiornoRicerca model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var prenotazioni = await _ricercheService.GetPrenotazioniByDettagliSoggiornoAsync(model.DettagliSoggiorno);

                if (prenotazioni != null && prenotazioni.Any())
                {
                    return RedirectToAction("TotByDettagliSoggiorno", new { dettagliSoggiorno = model.DettagliSoggiorno });
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


        [HttpGet("TotByDettagliSoggiorno")]
        public async Task<IActionResult> TotByDettagliSoggiorno(string dettagliSoggiorno)
        {
            if (string.IsNullOrWhiteSpace(dettagliSoggiorno))
            {
                return RedirectToAction("RicercaByDettagliSoggiorno");
            }

            try
            {
                var prenotazioni = await _ricercheService.GetPrenotazioniByDettagliSoggiornoAsync(dettagliSoggiorno);

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
