using ProgettoSettimanale.Models;

namespace ProgettoSettimanale.Services.Management
{
    public class VisualizzaService : CommonService, IVisualizzaService
    {
        private const string GET_ALL_CAMERE_COMMAND =
           "SELECT Id, Numero, Descrizione, Tipologia FROM [dbo].[Camera]";

        private const string GET_ALL_CLIENTE_COMMAND =
            "SELECT Id, CodiceFiscale, Cognome, Nome,  Citta, Provincia, Email, Telefono, Cellulare FROM [dbo].[Cliente]";

        private const string GET_ALL_PRENOTAZIONI_COMMAND =
            "SELECT Id, ClienteId, CameraId, DataPrenotazione, NumeroProgressivo, Anno, Dal, Al, CaparraConfirmatoria, Tariffa, DettagliSoggiorno " +
            "FROM [dbo].[Prenotazione]";

        private readonly ILogger<VisualizzaService> _logger;

        public VisualizzaService(IConfiguration configuration, ILogger<VisualizzaService> logger)
            : base(configuration.GetConnectionString("AuthDb"))
        {
            _logger = logger;
        }

        public List<Camera> GetAllCamere()
        {
            try
            {
                return ExecuteReader(
                    GET_ALL_CAMERE_COMMAND,
                    command => { },
                    reader => new Camera
                    {
                        Id = reader.GetInt32(0),
                        Numero = reader.GetInt32(1),
                        Descrizione = reader.IsDBNull(2) ? null : reader.GetString(2),
                        Tipologia = reader.IsDBNull(3) ? null : reader.GetString(3)
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle camere.");
                throw new Exception("Si è verificato un errore inatteso. Riprova più tardi.");
            }
        }

        public List<Cliente> GetAllClienti()
        {
            try
            {
                return ExecuteReader(
                    GET_ALL_CLIENTE_COMMAND,
                    command => { /* Nessun parametro aggiuntivo */ },
                    reader => new Cliente
                    {
                        Id = reader.GetInt32(0),
                        CodiceFiscale = reader.GetString(1),
                        Cognome = reader.GetString(2),
                        Nome = reader.GetString(3),
                        Citta = reader.IsDBNull(4) ? null : reader.GetString(4),
                        Provincia = reader.IsDBNull(5) ? null : reader.GetString(5),
                        Email = reader.IsDBNull(6) ? null : reader.GetString(6),
                        Telefono = reader.IsDBNull(7) ? null : reader.GetString(7),
                        Cellulare = reader.IsDBNull(8) ? null : reader.GetString(8),
                        
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero dei clienti.");
                throw new Exception("Si è verificato un errore inatteso. Riprova più tardi.");
            }
        }
        public List<Prenotazione> GetAllPrenotazioni()
        {
            try
            {
                return ExecuteReader(
                    GET_ALL_PRENOTAZIONI_COMMAND,
                    command => { /* Nessun parametro aggiuntivo */ },
                    reader => new Prenotazione
                    {
                        Id = reader.GetInt32(0),
                        ClienteId = reader.GetInt32(1),
                        CameraId = reader.GetInt32(2),
                        DataPrenotazione = reader.GetDateTime(3),
                        NumeroProgressivo = reader.GetInt32(4),
                        Anno = reader.GetInt32(5),
                        Dal = reader.GetDateTime(6),
                        Al = reader.GetDateTime(7),
                        CaparraConfirmatoria = reader.GetDecimal(8),
                        Tariffa = reader.GetDecimal(9),
                        DettagliSoggiorno = reader.IsDBNull(10) ? null : reader.GetString(10),
                        
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle prenotazioni.");
                throw new Exception("Si è verificato un errore inatteso. Riprova più tardi.");
            }
        }
    }
}
