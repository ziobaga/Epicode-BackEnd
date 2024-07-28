using Microsoft.Data.SqlClient;
using ProgettoSettimanale.Models;

namespace ProgettoSettimanale.Services.Management
{
    public class RicercaService : CommonService, IRicercaService 
    {
        private const string RICERCA_PRENOTAZIONE_BY_CF_COMMAND = @"
            SELECT p.*
            FROM Prenotazione p
            INNER JOIN Cliente c ON p.ClienteId = c.Id
            WHERE c.CodiceFiscale = @CodiceFiscale";


        private const string RICERCA_PRENOTAZIONE_BY_TIPO_PENSIONE_COMMAND = @"
            SELECT p.*
            FROM Prenotazione p
            WHERE p.DettagliSoggiorno = @DettagliSoggiorno";

        private readonly ILogger<RicercaService> _logger;

        public RicercaService(IConfiguration configuration, ILogger<RicercaService> logger)
            : base(configuration.GetConnectionString("AuthDb"))
        {
            _logger = logger;
        }

        public async Task<List<Prenotazione>> GetPrenotazioniByCFAsync(string codiceFiscale)
        {
            if (string.IsNullOrWhiteSpace(codiceFiscale))
            {
                throw new ArgumentException("Il codice fiscale non può essere nullo o vuoto.", nameof(codiceFiscale));
            }

            return await ExecuteQueryAsync(
                RICERCA_PRENOTAZIONE_BY_CF_COMMAND,
                command => command.Parameters.AddWithValue("@CodiceFiscale", codiceFiscale)
            );
        }

        public async Task<List<Prenotazione>> GetPrenotazioniByDettagliSoggiornoAsync(string dettagliSoggiorno)
        {
            if (string.IsNullOrWhiteSpace(dettagliSoggiorno))
            {
                throw new ArgumentException("Il tipo di pensione non può essere nullo o vuoto.", nameof(dettagliSoggiorno));
            }

            var validPensionTypes = new HashSet<string> { "Solo Pernottamento", "Pensione Completa", "Mezza Pensione" };
            if (!validPensionTypes.Contains(dettagliSoggiorno))
            {
                throw new ArgumentException($"Il tipo di pensione '{dettagliSoggiorno}' non è valido. I tipi validi sono: {string.Join(", ", validPensionTypes)}.");
            }

            return await ExecuteQueryAsync(
                RICERCA_PRENOTAZIONE_BY_TIPO_PENSIONE_COMMAND,
                command => command.Parameters.AddWithValue("@DettagliSoggiorno", dettagliSoggiorno)
            );
        }

        private async Task<List<Prenotazione>> ExecuteQueryAsync(string query, Action<SqlCommand> parameterizeCommand)
        {
            Func<SqlDataReader, Prenotazione> readAction = reader => new Prenotazione
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                ClienteId = reader.GetInt32(reader.GetOrdinal("ClienteId")),
                 CameraId = reader.GetInt32(reader.GetOrdinal("CameraId")),
                DataPrenotazione = reader.GetDateTime(reader.GetOrdinal("DataPrenotazione")),
                NumeroProgressivo = reader.GetInt32(reader.GetOrdinal("NumeroProgressivo")),
                Anno = reader.GetInt32(reader.GetOrdinal("Anno")),
                Dal = reader.GetDateTime(reader.GetOrdinal("Dal")),
                Al = reader.GetDateTime(reader.GetOrdinal("Al")),
                CaparraConfirmatoria = reader.GetDecimal(reader.GetOrdinal("CaparraConfirmatoria")),
                Tariffa = reader.GetDecimal(reader.GetOrdinal("Tariffa")),
                DettagliSoggiorno = reader.IsDBNull(reader.GetOrdinal("DettagliSoggiorno")) ? null : reader.GetString(reader.GetOrdinal("DettagliSoggiorno")),
                
               
            };

            try
            {
                return await ExecuteReaderAsync(
                    query,
                    parameterizeCommand,
                    readAction
                );
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Errore durante l'esecuzione della query.");
                throw;
            }
        }
    }
}
