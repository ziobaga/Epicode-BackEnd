using Project.Models;

namespace Project.Services.Management
{
    public class AddServizi : CommonService, IAddServizi
    {
        private readonly ILogger<AddServizi> _logger;

        private const string ADD_SERVIZI_COMMAND = @"
            INSERT INTO PrenotazioniServiziAgg
            (IdPrenotazione, IdServizioAgg, Data, Quantita, Prezzo) 
            VALUES (@IdPrenotazione, @IdServizioAgg, @Data, @Quantita, @Prezzo)";
        private const string GET_SERVIZI = "SELECT * FROM ServiziAgg";
        public AddServizi(IConfiguration configuration, ILogger<AddServizi> logger)
            : base(configuration.GetConnectionString("AuthDb"))
        {
            _logger = logger;
        }

        public PrenotazioneServizioAgg AddServizio(PrenotazioneServizioAgg prenotazioneServizioAgg, int idPrenotazione)
        {
            try
            {
                ExecuteNonQuery(
                    ADD_SERVIZI_COMMAND,
                    command =>
                    {
                        command.Parameters.AddWithValue("@IdPrenotazione", idPrenotazione);
                        command.Parameters.AddWithValue("@IdServizioAgg", prenotazioneServizioAgg.IdServizioAgg);
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

        public List<ServizioAgg> GetServizi()
        {
            try
            {
                return ExecuteReader(GET_SERVIZI, null, reader => new ServizioAgg
                {
                    IdServizioAgg = reader.GetInt32(0),
                    Descrizione = reader.GetString(1),
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero dei servizi aggiuntivi.");
                return null;
            }
        }
    }
}
