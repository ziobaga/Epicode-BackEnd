using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Services.Management;

namespace Project.Controllers.Management
{
    [Authorize]
    public class CreazioniController : Controller
    {
        private readonly ICreazioneService _creazioneService;
        private readonly ILogger<CreazioniController> _logger;

        public CreazioniController(ICreazioneService creazioneService, ILogger<CreazioniController> logger)
        {
            _creazioneService = creazioneService;
            _logger = logger;
        }


        public IActionResult CreaPersona()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreaPersona(Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return View(persona);
            }

            try
            {
                var createdPersona = _creazioneService.CreaPersona(persona);
                return RedirectToAction("Management", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la creazione della persona.");
                ModelState.AddModelError(string.Empty, "Si è verificato un errore. Riprova più tardi.");
                return View(persona);
            }
        }


        public IActionResult CreaCamera()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreaCamera(Camera camera)
        {
            if (!ModelState.IsValid)
            {
                return View(camera);
            }

            try
            {
                var createdCamera = _creazioneService.CreaCamera(camera);
                return RedirectToAction("Management", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la creazione della camera.");
                ModelState.AddModelError(string.Empty, "Si è verificato un errore. Riprova più tardi.");
                return View(camera);
            }
        }

        public IActionResult CreaPrenotazione()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreaPrenotazione(Prenotazione prenotazione)
        {
            if (!ModelState.IsValid)
            {
                return View(prenotazione);
            }

            try
            {
                var createdPrenotazione = _creazioneService.CreaPrenotazione(prenotazione);
                return RedirectToAction("Management", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la creazione della prenotazione.");
                ModelState.AddModelError(string.Empty, "Si è verificato un errore. Riprova più tardi.");
                return View(prenotazione);
            }
        }


    }
}
