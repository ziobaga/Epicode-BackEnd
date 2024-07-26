﻿using ProgettoSettimanale.Models.Auth;
using System.Security.Cryptography; 
using System.Text;

namespace ProgettoSettimanale.Services.Auth
{
    public class AuthService : CommonService, IAuthService
    {
        private const string LOGIN_COMMAND = "SELECT IdUtente, Username FROM Utenti WHERE Username = @Username AND Password = @Password";
        private const string REGISTER_COMMAND = "INSERT INTO Utenti (Username, Password) VALUES (@Username, @Password); SELECT SCOPE_IDENTITY();";

        private readonly ILogger<AuthService> _logger;

        public AuthService(IConfiguration configuration, ILogger<AuthService> logger) : base(configuration.GetConnectionString("AuthDb"))
        {
            _logger = logger;
        }

        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())  // Usa SHA256.Create() per ottenere un'istanza di SHA256
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        public Utente Login(string username, string password)
        {
            try
            {
                var hashedPassword = HashPassword(password);
                var users = ExecuteReader(LOGIN_COMMAND, command =>
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", hashedPassword);
                }, reader =>
                {
                    return new Utente
                    {
                        IdUtente = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Password = password
                    };
                });

                return users.FirstOrDefault();
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Si è verificato un errore inatteso durante il tentativo di login.");
                throw new Exception("Si è verificato un errore inatteso. Riprova più tardi.");
            }
        }

        public Utente Register(string username, string password)
        {
            try
            {
                var hashedPassword = HashPassword(password);
                var userId = ExecuteScalar<decimal>(REGISTER_COMMAND, command =>
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", hashedPassword);
                });

                return new Utente
                {
                    IdUtente = (int)userId,
                    Username = username,
                    Password = hashedPassword
                };
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Si è verificato un errore inatteso durante la registrazione dell'utente.");
                throw new Exception("Si è verificato un errore inatteso. Riprova più tardi.");
            }
        }
    }
}
