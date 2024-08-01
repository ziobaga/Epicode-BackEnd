using ProgettoSettimanale.Models;

namespace ProgettoSettimanale.Services.Auth
{
    public interface IAuthService
    {
        public Task<User> RegisterAsync(User user);
        public Task<User> LoginAsync(User user);
    }
}
