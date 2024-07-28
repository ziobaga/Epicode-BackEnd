using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Services.Management;

namespace Project.Controllers.Management
{
    [Authorize]
    public class VisualizzazioniController : Controller
    {
        private readonly IVisualizzaService _visualizzaCreazioniService;
        public VisualizzazioniController(IVisualizzaService visualizzaCreazioniService)
        {
            _visualizzaCreazioniService = visualizzaCreazioniService;
        }
        public IActionResult ListaPersone()
        {
            var persone = _visualizzaCreazioniService.GetAllPersone();
            return View(persone);
        }
        public IActionResult ListaCamere()
        {
            var camere = _visualizzaCreazioniService.GetAllCamere();
            return View(camere);
        }
        public IActionResult ListaPrenotazioni()
        {
            var prenotazioni = _visualizzaCreazioniService.GetAllPrenotazioni();
            return View(prenotazioni);
        }

    }
}
