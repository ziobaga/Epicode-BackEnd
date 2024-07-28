using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProgettoSettimanale.Models.Ricerca;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProgettoSettimanale.Services
{
    public class CheckoutService : CommonService, ICheckoutService
    {
        private readonly ILogger<CheckoutService> _logger;

        private const string TARIFFA_STANZA = @"
            SELECT 
                C.Numero, 
                P.Dal, 
                P.Al, 
                P.Tariffa,
                P.CaparraConfirmatoria
            FROM 
                Prenotazione AS P 
            INNER JOIN 
                Camera AS C ON P.CameraId = C.Id 
            WHERE 
                P.Id = @Id";

        private const string SERVIZI_BY_PRENOTAZIONE = @"
            SELECT 
                S.Nome, 
                PS.Data, 
                PS.Quantita, 
                PS.Prezzo 
            FROM 
                Servizio AS S 
            INNER JOIN 
                PrenotazioneServizi AS PS ON S.Id = PS.ServizioId 
            WHERE 
                PS.PrenotazioneId = @Id";

        private const string GET_IMPORTO = @"
            SELECT 
                (p.Tariffa - p.CaparraConfirmatoria + ISNULL(SUM(ps.Quantita * ps.Prezzo), 0)) AS ServizioPrezzo 
            FROM 
                Prenotazione AS p 
            LEFT JOIN 
                PrenotazioneServizi AS ps ON p.Id = ps.PrenotazioneId 
            WHERE 
                p.Id = @Id 
            GROUP BY 
                p.Tariffa, p.CaparraConfirmatoria";

        public CheckoutService(IConfiguration configuration, ILogger<CheckoutService> logger)
            : base(configuration.GetConnectionString("AuthDb"))
        {
            _logger = logger;
        }

        public async Task<CheckoutRicerca> PrenotazioneConImportoDaSaldare(int prenotazioneId)
        {
            var model = new CheckoutRicerca { PrenotazioneId = prenotazioneId };

            try
            {
                var stanzaDetails = await ExecuteReaderAsync<CameraRicerca>(
                    TARIFFA_STANZA,
                    cmd => cmd.Parameters.AddWithValue("@Id", prenotazioneId),
                    reader => new CameraRicerca
                    {
                        Numero = reader.GetInt32(reader.GetOrdinal("Numero")),
                        Dal = reader.GetDateTime(reader.GetOrdinal("Dal")),
                        Al = reader.GetDateTime(reader.GetOrdinal("Al")),
                        Tariffa = reader.GetDecimal(reader.GetOrdinal("Tariffa")),
                        CaparraConfirmatoria = reader.GetDecimal(reader.GetOrdinal("CaparraConfirmatoria"))
                    });

                if (stanzaDetails.Any())
                {
                    var stanza = stanzaDetails.First();
                    model.Numero = stanza.Numero;
                    model.Dal = stanza.Dal;
                    model.Al = stanza.Al;
                    model.Tariffa = stanza.Tariffa;
                    model.CaparraConfirmatoria = stanza.CaparraConfirmatoria;
                }

                var serviziAgg = await ExecuteReaderAsync<ServizioAggRicerca>(
                    SERVIZI_BY_PRENOTAZIONE,
                    cmd => cmd.Parameters.AddWithValue("@Id", prenotazioneId),
                    reader => new ServizioAggRicerca
                    {
                        Nome = reader.GetString(reader.GetOrdinal("Nome")),
                        Data = reader.IsDBNull(reader.GetOrdinal("Data")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("Data")),
                        Quantita = reader.IsDBNull(reader.GetOrdinal("Quantita")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("Quantita")),
                        Prezzo = reader.IsDBNull(reader.GetOrdinal("Prezzo")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("Prezzo"))
                    });

                model.ServiziAgg = serviziAgg;

                model.ImportoDaSaldare = await ExecuteScalarAsync<decimal>(
                    GET_IMPORTO,
                    cmd => cmd.Parameters.AddWithValue("@Id", prenotazioneId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero dei dettagli di prenotazione per ID {PrenotazioneId}", prenotazioneId);
                throw;
            }

            return model;
        }
    }
}
