using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProgettoSettimanale.Models;
using ProgettoSettimanale.Service;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace PROGETTO_S1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISpedizioniService _spedizioniService;

        public HomeController(ILogger<HomeController> logger, ISpedizioniService spedizioniService)
        {
            _logger = logger;
            _spedizioniService = spedizioniService;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CercaSpedizioni(string codiceFiscale, string partitaIVA)
        {
            try
            {
                if (!string.IsNullOrEmpty(codiceFiscale))
                {
                    var spedizioni = _spedizioniService.SpedizioniPerClientePrivato(codiceFiscale);
                    return View("SpedizioniPerClientePrivato", spedizioni);
                }
                else if (!string.IsNullOrEmpty(partitaIVA))
                {
                    var spedizioni = _spedizioniService.SpedizioniPerClienteAzienda(partitaIVA);
                    return View("SpedizioniPerClienteAzienda", spedizioni);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Inserisci un codice fiscale o una partita IVA valida.");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Si è verificato un errore durante la ricerca delle spedizioni. Riprova più tardi.");
                _logger.LogError(ex, "Errore durante la ricerca delle spedizioni.");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult SendEmail(EmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string smtpServer = "smtp.gmail.com";
                    int smtpPort = 587;
                    string smtpUsername = "";
                    string smtpPassword = "";

                    using (var client = new SmtpClient(smtpServer, smtpPort))
                    {
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                        client.EnableSsl = true;

                        var mailMessage = new MailMessage
                        {
                            From = new MailAddress(smtpUsername),
                            Subject = model.Subject,
                            Body = model.Message,
                            IsBodyHtml = true
                        };

                        mailMessage.To.Add(model.ToEmail);

                        client.Send(mailMessage);
                    }

                    return RedirectToAction("Index", new { success = true });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Si è verificato un errore durante l'invio dell'email. Riprova più tardi.");
                    _logger.LogError(ex, "Errore durante l'invio dell'email.");
                }
            }

            return View(model);
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}