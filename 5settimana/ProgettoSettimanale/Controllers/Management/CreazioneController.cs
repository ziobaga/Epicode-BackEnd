using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgettoSettimanale.Models;
using ProgettoSettimanale.Services.Management;

namespace ProgettoSettimanale.Controllers.Management
{
    [Authorize]
    public class CreazioneController : Controller
    {
        private readonly ICreazioneService _creazioneService;
        private readonly ILogger<CreazioneController> _logger;

        public CreazioneController(ICreazioneService creazioneService, ILogger<CreazioneController> logger)
        {
            _creazioneService = creazioneService;
            _logger = logger;
        }

        public IActionResult CreazioneCliente()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreazioneCliente(Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return View(cliente);
            }

            try
            {
                _creazioneService.CreazioneCliente(cliente);
                return RedirectToAction("Management", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la creazione del cliente.");
                ModelState.AddModelError(string.Empty, "Si è verificato un errore. Riprova più tardi.");
                return View(cliente);
            }
        }

        public IActionResult CreazioneCamera()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreazioneCamera(Camera camera)
        {
            if (!ModelState.IsValid)
            {
                return View(camera);
            }

            try
            {
                _creazioneService.CreazioneCamera(camera);
                return RedirectToAction("Management", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la creazione della camera.");
                ModelState.AddModelError(string.Empty, "Si è verificato un errore. Riprova più tardi.");
                return View(camera);
            }
        }

        public IActionResult CreazionePrenotazione()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreazionePrenotazione(Prenotazione prenotazione)
        {
            if (!ModelState.IsValid)
            {
                return View(prenotazione);
            }

            try
            {
                _creazioneService.CreazionePrenotazione(prenotazione);
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
