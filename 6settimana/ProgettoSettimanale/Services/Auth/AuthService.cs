using ProgettoSettimanale.Context;
using ProgettoSettimanale.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Extensions.Logging; // Aggiungi questa direttiva

namespace ProgettoSettimanale.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<AuthService> _logger; // Aggiungi il logger

        public AuthService(DataContext dataContext, ILogger<AuthService> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        public async Task<User> RegisterAsync(User user)
        {
            _logger.LogInformation("Hashing user password");
            user.Password = HashPassword(user.Password);

            _logger.LogInformation("Retrieving user role from database");
            var userRole = await _dataContext.Roles.Where(r => r.IdRole == 2).FirstOrDefaultAsync();

            if (userRole == null)
            {
                _logger.LogError("User role not found");
                throw new Exception("User role not found");
            }

            _logger.LogInformation("Adding user role to user");
            user.Roles.Add(userRole);

            _logger.LogInformation("Adding user to database");
            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();

            _logger.LogInformation("User registered successfully");
            return user;
        }

        public async Task<User> LoginAsync(User user)
        {
            _logger.LogInformation("Hashing user password for login");
            string hashedPassword = HashPassword(user.Password);

            _logger.LogInformation("Retrieving user from database");
            var existingUser = await _dataContext.Users
                 .Include(u => u.Roles)
                 .Where(u => u.Username == user.Username && u.Password == hashedPassword || u.Password == user.Password)
                 .FirstOrDefaultAsync();

            if (existingUser == null)
            {
                _logger.LogWarning("Invalid username or password");
                throw new Exception("Invalid username or password");
            }

            _logger.LogInformation("User logged in successfully");
            return existingUser;
        }
    }
}
