using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using venerdì.Models;
using venerdì.Services;

namespace venerdì.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAnagraficaService _anagraficaService;
        private readonly IViolazioneService _violazioneService;
        private readonly IVerbaleService _verbaleService;

        public HomeController(ILogger<HomeController> logger, IAnagraficaService anagraficaService, IViolazioneService violazioneService, IVerbaleService verbaleService)
        {
            _logger = logger;
            _anagraficaService = anagraficaService;
            _violazioneService = violazioneService;
            _verbaleService = verbaleService;

        }

        public IActionResult Index()
        {
            return View();
        }



        //ANAGRAFICA

        public IActionResult CreaAnagrafica()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAnagrafica(Anagrafica anagrafica)
        {
            _logger.LogInformation("Received request to create Anagrafica.");

            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Model state is valid. Creating Anagrafica.");
                    _anagraficaService.Create(anagrafica);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating Anagrafica");
                    ModelState.AddModelError("", "Error creating Anagrafica: " + ex.Message);
                }
            }
            else
            {
                _logger.LogWarning("Model state is invalid.");
            }
            return View(anagrafica);
        }



        //VIOLAZIONE
        public IActionResult CreaViolazione()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreaViolazione(TipoViolazione violazione)
        {
            if (ModelState.IsValid)
            {
                _violazioneService.Create(violazione);
                return RedirectToAction("Index");
            }
            return View(violazione);
        }

        public IActionResult TutteLeViolazioni()
        {
            var violazioni = _violazioneService.GetAllViolazioni();
            return View(violazioni);
        }

        public IActionResult Sopra10Punti()
        {
            var violazioni = _violazioneService.GetViolazioneOver10Punti();
            return View(violazioni);
        }

        public IActionResult Sopra400Euro()
        {
            var violazioni = _violazioneService.GetViolazioneOver400Euro();
            return View(violazioni);
        }

       
        //VERBALE
        public IActionResult CreaVerbale()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreaVerbale(Verbale verbale)
        {
            if (ModelState.IsValid)
            {
                _verbaleService.Create(verbale);
                return RedirectToAction("Index");
            }
            return View(verbale);
        }


        public IActionResult VerbaliTrasgressori()
        {
            var verbaliByTrasgressore = _verbaleService.GetAllVerbaliByTrasgressore();
            return View(verbaliByTrasgressore);
        }


        public IActionResult PuntiDecurtatiTrasgressori()
        {
            var trasgressoreByPuntiDecurtati = _anagraficaService.GetAllTrasgressoreByPuntiDecurtati();
            return View(trasgressoreByPuntiDecurtati);
        }



        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
