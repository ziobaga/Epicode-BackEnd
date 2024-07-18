using ProgettoSettimanale.Models;

namespace ProgettoSettimanale.Service
{
    public interface IAuthService
    {
        Users Login(string username, string password);
        Users Register(string username, string password);
        List<string> GetUserRoles(int userId); // Nuovo metodo per ottenere i ruoli
    }
}
