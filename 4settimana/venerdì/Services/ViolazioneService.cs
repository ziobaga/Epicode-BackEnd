using Microsoft.Data.SqlClient;
using venerdì.Models;

namespace venerdì.Services
{
    public class ViolazioneService : IViolazioneService
    {
        private readonly string _connectionString;



        public ViolazioneService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AuthDb");
        }

        private const string CREATE_VIOLAZIONE_COMMAND = "INSERT INTO [dbo].[TIPO_VIOLAZIONE] (Descrizione) OUTPUT INSERTED.idviolazione VALUES (@Descrizione)";

        public TipoViolazione Create(TipoViolazione violazione)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(CREATE_VIOLAZIONE_COMMAND, connection))
                    {
                        command.Parameters.AddWithValue("@Descrizione", violazione.Descrizione);

                        violazione.IDViolazione = (int)command.ExecuteScalar();
                    }
                }
                return violazione;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating Violazione: " + ex.Message);
            }
        }


        private const string GET_ALL_VIOLAZIONI_COMMAND = "SELECT idviolazione, Descrizione FROM [dbo].[TIPO_VIOLAZIONE]";
        public List<TipoViolazione> GetAllViolazioni()
        {
            var violazioni = new List<TipoViolazione>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(GET_ALL_VIOLAZIONI_COMMAND, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var violazione = new TipoViolazione
                                {
                                    IDViolazione = reader.GetInt32(0),
                                    Descrizione = reader.GetString(1)
                                };
                                violazioni.Add(violazione);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving violazioni: " + ex.Message);
            }

            return violazioni;
        }


        private const string GET_VIOLAZIONI_OVER_10_PUNTI_COMMAND = @"
            SELECT 
                v.Importo, 
                a.Nome, 
                a.Cognome, 
                v.DataViolazione, 
                v.DecurtamentoPunti
            FROM [dbo].[VERBALE] v
            JOIN [dbo].[ANAGRAFICA] a ON v.IDAnagrafica = a.IDAnagrafica
            WHERE v.DecurtamentoPunti > 10
            ORDER BY v.DecurtamentoPunti DESC;";
        public List<ViolazioneSopra10Punti> GetViolazioneOver10Punti()
        {
            var result = new List<ViolazioneSopra10Punti>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(GET_VIOLAZIONI_OVER_10_PUNTI_COMMAND, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var violazione = new ViolazioneSopra10Punti
                                {
                                    Importo = reader.GetDecimal(0),
                                    Nome = reader.GetString(1),
                                    Cognome = reader.GetString(2),
                                    DataViolazione = reader.GetDateTime(3),
                                    DecurtamentoPunti = reader.GetInt32(4)
                                };
                                result.Add(violazione);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving violations with over 10 points: " + ex.Message);
            }

            return result;
        }

        private const string GET_VIOLAZIONI_OVER_400_IMPORTO_COMMAND = @"
    SELECT 
        a.Nome, 
        a.Cognome, 
        v.DataViolazione,                
        v.Importo
    FROM [dbo].[VERBALE] v
    JOIN [dbo].[ANAGRAFICA] a ON v.IDAnagrafica = a.IDAnagrafica
    WHERE v.Importo > 400
    ORDER BY v.Importo DESC;";

        public List<ViolazioneSopra400Euro> GetViolazioneOver400Euro()
        {
            var result = new List<ViolazioneSopra400Euro>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(GET_VIOLAZIONI_OVER_400_IMPORTO_COMMAND, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var violazione = new ViolazioneSopra400Euro
                                {
                                    Nome = reader.GetString(0),
                                    Cognome = reader.GetString(1),
                                    DataViolazione = reader.GetDateTime(2),
                                    Importo = reader.GetDecimal(3)
                                };
                                result.Add(violazione);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving violations with import over 400 euros: " + ex.Message);
            }

            return result;
        }

    }
}
