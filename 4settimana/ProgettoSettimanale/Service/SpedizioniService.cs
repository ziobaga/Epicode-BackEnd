using Microsoft.Data.SqlClient;
using ProgettoSettimanale.Models;

namespace ProgettoSettimanale.Service
{
    public class SpedizioniService : ISpedizioniService
    {
        private readonly string _connectionString;

        public SpedizioniService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Authdb");
        }

        public List<Spedizione> SpedizioniPerClientePrivato(string codiceFiscale)
        {
            const string query = @"
                SELECT s.* ,st.DataOraAggiornamento
                FROM Spedizioni s
                JOIN ClientiPrivato cp ON s.FK_ClientePrivato = cp.IdClientePriv
                JOIN StatoSpedizione st ON s.IdSpedizione = st.FK_IdSpedizione
                WHERE cp.CF = @CodiceFiscale
                ORDER BY st.DataOraAggiornamento DESC;";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CodiceFiscale", codiceFiscale);

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
                                    DataConsegnaPrev = reader.GetDateTime(reader.GetOrdinal("DataConsegnaPrev")),
                                    DataOraAggiornamento = reader.GetDateTime(reader.GetOrdinal("DataOraAggiornamento"))

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

        public List<Spedizione> SpedizioniPerClienteAzienda(string partitaIVA)
        {
            const string query = @"
        SELECT s.*, st.DataOraAggiornamento
        FROM Spedizioni s
        JOIN StatoSpedizione st ON s.IdSpedizione = st.IdStatoSpedizione
        JOIN ClientiAzienda ca ON s.FK_ClienteAzienda = ca.IdClienteAzienda
        WHERE ca.PIVA = @PartitaIVA
        ORDER BY st.DataOraAggiornamento DESC;";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PartitaIVA", partitaIVA);

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
                                    DataConsegnaPrev = reader.GetDateTime(reader.GetOrdinal("DataConsegnaPrev")),
                                    DataOraAggiornamento = reader.GetDateTime(reader.GetOrdinal("DataOraAggiornamento"))
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
    }
}
