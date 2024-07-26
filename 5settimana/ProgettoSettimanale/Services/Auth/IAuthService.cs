using ProgettoSettimanale.Models.Auth;

namespace ProgettoSettimanale.Services.Auth
{
    public interface IAuthService
    {
        Utente Login(string username, string password);
        Utente Register(string username, string password);
    }
}
