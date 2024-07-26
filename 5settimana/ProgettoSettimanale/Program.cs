using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ProgettoSettimanale.Services;
using ProgettoSettimanale.Services.Auth;
using ProgettoSettimanale.Services.Management;

namespace ProgettoSettimanale
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            //AUTH
            builder.Services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Auth/Register";
                });

            // Registrazione dei servizi
            builder.Services
                .AddScoped<IAuthService, AuthService>()
                .AddScoped<IServizioService, ServizioService>()
                .AddScoped<ICreazioneService, CreazioneService>()
                .AddScoped<IVisualizzaService, VisualizzaService>()
                .AddScoped<IRicercaService, RicercaService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
