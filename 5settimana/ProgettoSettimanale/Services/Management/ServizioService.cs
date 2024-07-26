using ProgettoSettimanale.Models;

namespace ProgettoSettimanale.Services.Management
{
    public class ServizioService : CommonService, IServizioService
    {
        private readonly ILogger<ServizioService> _logger;

        // SQL commands
        private const string ADD_SERVIZI_AGG_COMMAND = @"
            INSERT INTO PrenotazioneServizi (PrenotazioneId, ServizioId, Data, Quantita, Prezzo) 
            VALUES (@PrenotazioneId, @ServizioId, @Data, @Quantita, @Prezzo)";
        private const string GET_SERVIZI_AGG_COMMAND = "SELECT Id, Nome, Prezzo FROM Servizio";

        public ServizioService(IConfiguration configuration, ILogger<ServizioService> logger)
            : base(configuration.GetConnectionString("AuthDb"))
        {
            _logger = logger;
        }

        public PrenotazioneServizi AddServizioAgg(PrenotazioneServizi prenotazioneServizioAgg, int idPrenotazione)
        {
            try
            {
                ExecuteNonQuery(
                    ADD_SERVIZI_AGG_COMMAND,
                    command =>
                    {
                        command.Parameters.AddWithValue("@PrenotazioneId", idPrenotazione);
                        command.Parameters.AddWithValue("@ServizioId", prenotazioneServizioAgg.ServizioId);
                        command.Parameters.AddWithValue("@Data", prenotazioneServizioAgg.Data);
                        command.Parameters.AddWithValue("@Quantita", prenotazioneServizioAgg.Quantita);
                        command.Parameters.AddWithValue("@Prezzo", prenotazioneServizioAgg.Prezzo);
                    });

                return prenotazioneServizioAgg;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante l'aggiunta di un servizio aggiuntivo.");
                throw new Exception("Si è verificato un errore inatteso. Riprova più tardi.");
            }
        }

        public List<Servizio> GetServiziAgg()
        {
            try
            {
                return ExecuteReader(GET_SERVIZI_AGG_COMMAND, null, reader => new Servizio
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Prezzo = reader.GetDecimal(2)
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero dei servizi aggiuntivi.");
                return new List<Servizio>();
            }
        }
    }
}
