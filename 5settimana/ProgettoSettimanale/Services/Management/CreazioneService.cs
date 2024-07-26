using Microsoft.Data.SqlClient;
using ProgettoSettimanale.Models;
using System.Data;

namespace ProgettoSettimanale.Services.Management
{
    public class CreazioneService : CommonService, ICreazioneService
    {
        private const string CREAZIONE_CLIENTE_COMMAND = "INSERT INTO [dbo].[Cliente] " +
            "(CodiceFiscale,Cognome,Nome,Citta,Provincia,Email,Telefono,Cellulare) " +
            "OUTPUT INSERTED.Id " +
            "VALUES (@CodiceFiscale, @Cognome, @Nome, @Citta, @Provincia, @Email, @Telefono, @Cellulare)";

        private const string CREAZIONE_CAMERA_COMMAND = "INSERT INTO [dbo].[Camera] " +
            "(Numero, Descrizione, Tipologia) " +
            "OUTPUT INSERTED.Id " +
            "VALUES (@Numero, @Descrizione, @Tipologia)";

        private const string CREAZIONE_PRENOTAZIONE_COMMAND = "INSERT INTO [dbo].[Prenotazione] " +
            "(ClienteId, CameraId, DataPrenotazione, NumeroProgressivo, Anno, Dal, Al, CaparraConfirmatoria, Tariffa, DettagliSoggiorno) " +
            "OUTPUT INSERTED.Id " +
            "VALUES (@ClienteId, @CameraId, @DataPrenotazione, @NumeroProgressivo, @Anno, @Dal, @Al, @CaparraConfirmatoria, @Tariffa, @DettagliSoggiorno)";

        private const string GET_NEXT_NUM_PROGRESSIVO_COMMAND = "GetNextNumProgressivo";

        private readonly ILogger<CreazioneService> _logger;

        public CreazioneService(IConfiguration configuration, ILogger<CreazioneService> logger) : base(configuration.GetConnectionString("AuthDb"))
        {
            _logger = logger;
        }

        public Cliente CreazioneCliente(Cliente cliente)
        {
            try
            {
                var clienteId = ExecuteScalar<int>(CREAZIONE_CLIENTE_COMMAND, command =>
                {
                    command.Parameters.AddWithValue("@CodiceFiscale", cliente.CodiceFiscale);
                    command.Parameters.AddWithValue("@Cognome", cliente.Cognome);
                    command.Parameters.AddWithValue("@Nome", cliente.Nome);
                    command.Parameters.AddWithValue("@Citta", cliente.Citta ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Provincia", cliente.Provincia ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Email", cliente.Email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Telefono", cliente.Telefono ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Cellulare", cliente.Cellulare ?? (object)DBNull.Value);
                });

                return new Cliente
                {
                    Id = clienteId,
                    CodiceFiscale = cliente.CodiceFiscale,
                    Cognome = cliente.Cognome,
                    Nome = cliente.Nome,
                    Citta = cliente.Citta,
                    Provincia = cliente.Provincia,
                    Email = cliente.Email,
                    Telefono = cliente.Telefono,
                    Cellulare = cliente.Cellulare
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Si è verificato un errore inatteso durante la creazione della persona.");
                throw new Exception("Si è verificato un errore inatteso. Riprova più tardi.");
            }
        }

        public Camera CreazioneCamera(Camera camera)
        {
            try
            {
                var cameraId = ExecuteScalar<int>(CREAZIONE_CAMERA_COMMAND, command =>
                {
                    command.Parameters.AddWithValue("@Numero", camera.Numero);
                    command.Parameters.AddWithValue("@Descrizione", camera.Descrizione ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Tipologia", camera.Tipologia ?? (object)DBNull.Value);
                });

                return new Camera
                {
                    Id = cameraId,
                    Numero = camera.Numero,
                    Descrizione = camera.Descrizione,
                    Tipologia = camera.Tipologia
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Si è verificato un errore inatteso durante la creazione della camera.");
                throw new Exception("Si è verificato un errore inatteso. Riprova più tardi.");
            }
        }

        public Prenotazione CreazionePrenotazione(Prenotazione prenotazione)
        {
            try
            {
                prenotazione.DataPrenotazione = DateTime.Now;
                prenotazione.Anno = prenotazione.Dal.Year;

                int nextNumProgressivo;
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(GET_NEXT_NUM_PROGRESSIVO_COMMAND, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Anno", prenotazione.Anno);

                        var outputParam = new SqlParameter("@NextNumeroProgressivo", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParam);

                        command.ExecuteNonQuery();
                        nextNumProgressivo = (int)outputParam.Value;
                    }
                }

                prenotazione.NumeroProgressivo = nextNumProgressivo;

                var prenotazioneId = ExecuteScalar<int>(CREAZIONE_PRENOTAZIONE_COMMAND, command =>
                {
                    command.Parameters.AddWithValue("@ClienteId", prenotazione.ClienteId);
                    command.Parameters.AddWithValue("@CameraId", prenotazione.CameraId);
                    command.Parameters.AddWithValue("@DataPrenotazione", prenotazione.DataPrenotazione);
                    command.Parameters.AddWithValue("@NumeroProgressivo", prenotazione.NumeroProgressivo);
                    command.Parameters.AddWithValue("@Anno", prenotazione.Anno);
                    command.Parameters.AddWithValue("@Dal", prenotazione.Dal);
                    command.Parameters.AddWithValue("@Al", prenotazione.Al);
                    command.Parameters.AddWithValue("@CaparraConfirmatoria", prenotazione.CaparraConfirmatoria);
                    command.Parameters.AddWithValue("@Tariffa", prenotazione.Tariffa);
                    command.Parameters.AddWithValue("@DettagliSoggiorno", prenotazione.DettagliSoggiorno ?? (object)DBNull.Value);
                });

                return new Prenotazione
                {
                    Id = prenotazioneId,
                    ClienteId = prenotazione.ClienteId,
                    CameraId = prenotazione.CameraId,
                    DataPrenotazione = prenotazione.DataPrenotazione,
                    NumeroProgressivo = prenotazione.NumeroProgressivo,
                    Anno = prenotazione.Anno,
                    Dal = prenotazione.Dal,
                    Al = prenotazione.Al,
                    CaparraConfirmatoria = prenotazione.CaparraConfirmatoria,
                    Tariffa = prenotazione.Tariffa,
                    DettagliSoggiorno = prenotazione.DettagliSoggiorno
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Si è verificato un errore inatteso durante la creazione della prenotazione.");
                throw new Exception("Si è verificato un errore inatteso. Riprova più tardi.");
            }
        }
    }
}
