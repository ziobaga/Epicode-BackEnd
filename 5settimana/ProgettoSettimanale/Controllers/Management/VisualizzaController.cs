using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgettoSettimanale.Services.Management;

namespace ProgettoSettimanale.Controllers.Management
{
    [Authorize]
    public class VisualizzaController : Controller
    {
        private readonly IVisualizzaService _visualizzaCreazioniService;
        public VisualizzaController(IVisualizzaService visualizzaCreazioniService)
        {
            _visualizzaCreazioniService = visualizzaCreazioniService;
        }
        public IActionResult TotClienti()
        {
            var clienti = _visualizzaCreazioniService.GetAllClienti();
            return View(clienti);
        }
        public IActionResult TotCamere()
        {
            var camere = _visualizzaCreazioniService.GetAllCamere();
            return View(camere);
        }
        public IActionResult TotPrenotazioni()
        {
            var prenotazioni = _visualizzaCreazioniService.GetAllPrenotazioni();
            return View(prenotazioni);
        }

    }
}
