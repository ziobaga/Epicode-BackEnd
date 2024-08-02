using ProgettoSettimanale.Context;
using ProgettoSettimanale.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;

namespace ProgettoSettimanale.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _dataContext;
        public AuthService(DataContext dataContext)
        {
            _dataContext = dataContext;
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
            user.Password = HashPassword(user.Password);
            var userRole = await _dataContext.Roles.Where(r => r.IdRole == 1).FirstOrDefaultAsync();
            user.Roles.Add(userRole);
            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();
            return user;
        }


        public async Task<User> LoginAsync(User user)
        {
            string hashedPassword = HashPassword(user.Password);

            var existingUser = await _dataContext.Users
                 .Include(u => u.Roles)
                 .Where(u => u.Username == user.Username && u.Password == hashedPassword || u.Password == user.Password) 
                 .FirstOrDefaultAsync();

            if (existingUser == null)
            {
                throw new Exception("Password invalida o username");
            }
            return existingUser;
        }
    }
}
