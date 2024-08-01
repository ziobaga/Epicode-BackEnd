using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ProgettoSettimanale.Context;
using ProgettoSettimanale.Services.Auth;
using ProgettoSettimanale.Services.Cart;
using ProgettoSettimanale.Services.Ingredients;

using ProgettoSettimanale.Services.Products;



namespace ProgettoSettimanale
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var conn = builder.Configuration.GetConnectionString("AuthDb");
            builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(conn));

            //AUTH
            builder.Services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Auth/Register";
                });

            //SERVIZI
            builder.Services
                .AddScoped<IAuthService, AuthService>()
                .AddScoped<IIngredientService, IngredientService>()
                .AddScoped<IProductService, ProductService>()
                .AddScoped<ICartService, CartService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
