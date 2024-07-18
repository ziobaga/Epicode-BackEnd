using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgettoSettimanale.Models;
using ProgettoSettimanale.Service;
using System.Security.Claims;

namespace PROGETTO_S1.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IAdminService _adminService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAuthService authService, IAdminService adminService, ILogger<AccountController> logger)
        {
            _authService = authService;
            _logger = logger;
            _adminService = adminService;

        }

        //////////////////////////////////////////////////// //LOGIN///////////////////////////////////////////////////////////
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Users users)
        {
            try
            {
                var user = _authService.Login(users.Username, users.Password);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return RedirectToAction("Index", "Home");
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim("UserId", user.Id.ToString())
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login");
                ModelState.AddModelError(string.Empty, "An error occurred. Please try again.");
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }


        ////////////////////////////////////////////REGISTER////////////////////////////////////////////////////////////
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(Users users)
        {
            try
            {
                var user = _authService.Register(users.Username, users.Password);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid register attempt.");
                    return View(users);
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username)
                };

                var roles = _authService.GetUserRoles(user.Id);
                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Registration failed.");
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return View(users);
            }
        }
        //     //     //     //     //     //     //  LOGOUT //     //     //     //     //     //     //     //     //
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        ////////////////////////////////////////////////////////////////////////PAGINA GESTIONALE ADMIN//////////////////////////////////////////////////////////////////////////

        [Authorize(Policy = "AdminPolicy")]
        public IActionResult AdminPage()
        {
            var spedizioni = _adminService.SpedizioniInConsegnaOggi();
            var totSpedizioniNonConsegnate = _adminService.TotSpedizioniNonConsegnate();
            var spedizioniPerCitta = _adminService.SpedizioniPerCitta();
            var allSpedizioni = _adminService.GetAllSpedizioni();
            var allAzienda = _adminService.GetAllAzienda();
            var allPrivati = _adminService.GetAllPrivato();

            var model = new AdminPageViewModel
            {
                Spedizioni = spedizioni,
                TotSpedizioniNonConsegnate = totSpedizioniNonConsegnate,
                SpedizioniPerCitta = spedizioniPerCitta,
                GetAllSpedizioni = allSpedizioni,
                GetAllAzienda = allAzienda,
                GetAllPrivati = allPrivati
            };

            return View(model);
        }
    }
}