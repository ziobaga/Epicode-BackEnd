using Microsoft.Data.SqlClient;
using ProgettoSettimanale.Models;

namespace ProgettoSettimanale.Service
{
    public class AdminService : IAdminService
    {
        private readonly string _connectionString;

        private const string SPEDIZIONI_IN_CONSEGNA_COMMAND = @"
            SELECT s.IdSpedizione, s.FK_ClienteAzienda, s.FK_ClientePrivato, s.NumId, s.DataSpedizione, 
                   s.Peso, s.CittaDestinatario, s.Indirizzo, s.NomeDestinatario, s.CostoSpedizione, 
                   s.DataConsegnaPrev
            FROM Spedizioni s
            JOIN StatoSpedizione st ON s.IdSpedizione = st.FK_IdSpedizione
            WHERE st.Stato = 'In Consegna'
            AND CONVERT(DATE, s.DataConsegnaPrev) = CAST(GETDATE() AS DATE);";

        private const string TOTALE_SPEDIZIONI_NON_CONSEGNATE_COMMAND = @"
        SELECT COUNT(*) FROM Spedizioni s
        JOIN StatoSpedizione st ON s.IdSpedizione = st.FK_IdSpedizione
        WHERE st.Stato != 'Consegnato';";

        private const string SPEDIZIONI_PER_CITTA_COMMAND = @"
            SELECT CittaDestinatario, COUNT(*) AS NumeroTotaleSpedizioni
           FROM Spedizioni
           GROUP BY CittaDestinatario;";

        private const string TUTTE_LE_SPEDIZIONI_COMMAND = @"
            SELECT * FROM Spedizioni;";

        private const string TUTTI_GLI_UTENTIAZIENDA_COMMAND = @"
            SELECT * FROM ClientiAzienda;";

        private const string TUTTI_GLI_UTENTIPRIVATI_COMMAND = @"
            SELECT * FROM ClientiPrivato;";

        public AdminService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Authdb");
        }

        public List<Spedizione> SpedizioniInConsegnaOggi()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(SPEDIZIONI_IN_CONSEGNA_COMMAND, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            var spedizioni = new List<Spedizione>();
                            while (reader.Read())
                            {
                                var spedizione = new Spedizione
                                {
                                    IdSpedizione = reader.GetInt32(reader.GetOrdinal("IdSpedizione")),
                                    FK_ClienteAzienda = reader.IsDBNull(reader.GetOrdinal("FK_ClienteAzienda")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("FK_ClienteAzienda")),
                                    FK_ClientePrivato = reader.IsDBNull(reader.GetOrdinal("FK_ClientePrivato")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("FK_ClientePrivato")),
                                    NumId = reader.GetInt32(reader.GetOrdinal("NumId")),
                                    DataSpedizione = reader.GetDateTime(reader.GetOrdinal("DataSpedizione")),
                                    Peso = reader.GetDecimal(reader.GetOrdinal("Peso")),
                                    CittaDestinatario = reader.GetString(reader.GetOrdinal("CittaDestinatario")),
                                    Indirizzo = reader.GetString(reader.GetOrdinal("Indirizzo")),
                                    NomeDestinatario = reader.GetString(reader.GetOrdinal("NomeDestinatario")),
                                    CostoSpedizione = reader.GetDecimal(reader.GetOrdinal("CostoSpedizione")),
                                    DataConsegnaPrev = reader.GetDateTime(reader.GetOrdinal("DataConsegnaPrev"))
                                };
                                spedizioni.Add(spedizione);
                            }
                            return spedizioni;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int TotSpedizioniNonConsegnate()
        {
            const string TOTALE_SPEDIZIONI_NON_CONSEGNATE_COMMAND = @"
        SELECT COUNT(*) AS TotaleSpedizioniNonConsegnate
        FROM Spedizioni s
        JOIN StatoSpedizione st ON s.IdSpedizione = st.FK_IdSpedizione
        WHERE st.Stato != 'Consegnato';";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(TOTALE_SPEDIZIONI_NON_CONSEGNATE_COMMAND, connection))
                    {
                        var totaleSpedizioniNonConsegnate = (int)command.ExecuteScalar();
                        return totaleSpedizioniNonConsegnate;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<SpedizionePerCittaResult> SpedizioniPerCitta()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(SPEDIZIONI_PER_CITTA_COMMAND, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            var results = new List<SpedizionePerCittaResult>();
                            while (reader.Read())
                            {
                                var result = new SpedizionePerCittaResult
                                {
                                    CittaDestinatario = reader.GetString(reader.GetOrdinal("CittaDestinatario")),
                                    NumeroTotaleSpedizioni = reader.GetInt32(reader.GetOrdinal("NumeroTotaleSpedizioni"))
                                };
                                results.Add(result);
                            }
                            return results;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Spedizione> GetAllSpedizioni()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(TUTTE_LE_SPEDIZIONI_COMMAND, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            var spedizioni = new List<Spedizione>();
                            while (reader.Read())
                            {
                                var spedizione = new Spedizione
                                {
                                    IdSpedizione = reader.GetInt32(reader.GetOrdinal("IdSpedizione")),
                                    FK_ClienteAzienda = reader.IsDBNull(reader.GetOrdinal("FK_ClienteAzienda")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("FK_ClienteAzienda")),
                                    FK_ClientePrivato = reader.IsDBNull(reader.GetOrdinal("FK_ClientePrivato")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("FK_ClientePrivato")),
                                    NumId = reader.GetInt32(reader.GetOrdinal("NumId")),
                                    DataSpedizione = reader.GetDateTime(reader.GetOrdinal("DataSpedizione")),
                                    Peso = reader.GetDecimal(reader.GetOrdinal("Peso")),
                                    CittaDestinatario = reader.GetString(reader.GetOrdinal("CittaDestinatario")),
                                    Indirizzo = reader.GetString(reader.GetOrdinal("Indirizzo")),
                                    NomeDestinatario = reader.GetString(reader.GetOrdinal("NomeDestinatario")),
                                    CostoSpedizione = reader.GetDecimal(reader.GetOrdinal("CostoSpedizione")),
                                    DataConsegnaPrev = reader.GetDateTime(reader.GetOrdinal("DataConsegnaPrev"))
                                };
                                spedizioni.Add(spedizione);
                            }
                            return spedizioni;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<ClienteAzienda> GetAllAzienda()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(TUTTI_GLI_UTENTIAZIENDA_COMMAND, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            var azienda = new List<ClienteAzienda>();
                            while (reader.Read())
                            {
                                var az = new ClienteAzienda
                                {
                                    IdClienteAzienda = reader.GetInt32(reader.GetOrdinal("IdClienteAzienda")),
                                    Nome = reader.GetString(reader.GetOrdinal("Nome")),
                                    Sede = reader.GetString(reader.GetOrdinal("Sede")),
                                    Intestatario = reader.GetString(reader.GetOrdinal("Intestatario")),
                                    PIVA = reader.GetString(reader.GetOrdinal("PIVA")),
                                };
                                azienda.Add(az);
                            }
                            return azienda;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<ClientePrivato> GetAllPrivato()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(TUTTI_GLI_UTENTIPRIVATI_COMMAND, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            var privato = new List<ClientePrivato>();
                            while (reader.Read())
                            {
                                var pv = new ClientePrivato
                                {
                                    IdClientePriv = reader.GetInt32(reader.GetOrdinal("IdClientePriv")),
                                    Nome = reader.GetString(reader.GetOrdinal("Nome")),
                                    Cognome = reader.GetString(reader.GetOrdinal("Cognome")),
                                    DataNascita = reader.GetDateTime(reader.GetOrdinal("DataNascita")),
                                    CF = reader.GetString(reader.GetOrdinal("CF")),
                                };
                                privato.Add(pv);
                            }
                            return privato;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
