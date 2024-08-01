using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ProgettoSettimanale.Models;
using ProgettoSettimanale.Services.Auth;
using System.Security.Claims;

namespace ProgettoSettimanale.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                await _authService.RegisterAsync(user);
                return RedirectToAction("Login", "Auth");
            }
            catch (Exception ex)
            {
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
