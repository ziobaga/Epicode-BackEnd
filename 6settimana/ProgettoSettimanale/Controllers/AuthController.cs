using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Aggiungi questa direttiva
using ProgettoSettimanale.Models;
using ProgettoSettimanale.Services.Auth;
using System.Security.Claims;

namespace ProgettoSettimanale.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger; // Aggiungi il logger

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid");
                return View(user);
            }

            try
            {
                _logger.LogInformation("Attempting to register user");
                var registeredUser = await _authService.RegisterAsync(user);

                if (registeredUser == null)
                {
                    _logger.LogError("User registration failed");
                    ModelState.AddModelError("", "Registration failed");
                    return View(user);
                }

                _logger.LogInformation("User registered successfully");
                return RedirectToAction("Login", "Auth");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred during registration");
                ModelState.AddModelError("", "An error occurred during registration");
                return View(user);
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            try
            {
                var existingUser = await _authService.LoginAsync(user);
                if (existingUser == null)
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                    return View(user);
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, existingUser.Username),
                    new Claim(ClaimTypes.NameIdentifier, existingUser.IdUser.ToString())
                };

                existingUser.Roles.ForEach(r =>
                {
                    claims.Add(new Claim(ClaimTypes.Role, r.Name));
                });

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("ListProducts", "Product");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred during login");
                return View(user);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }
    }
}
