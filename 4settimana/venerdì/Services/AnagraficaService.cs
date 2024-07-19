using venerdì.Models;
using Microsoft.Data.SqlClient;

namespace venerdì.Services
{
    public class AnagraficaService : IAnagraficaService
    {
        private readonly string _connectionString;
        private readonly ILogger<AnagraficaService> _logger;

        private const string CREATE_ANAGRAFICA_COMMAND = "INSERT INTO [dbo].[ANAGRAFICA] " +
            "(Cognome, Nome, Indirizzo, Città, CAP, Cod_Fisc) " +
            "OUTPUT INSERTED.IDAnagrafica " +
            "VALUES (@Cognome, @Nome, @Indirizzo, @Città, @CAP, @Cod_Fisc)";

        public AnagraficaService(IConfiguration configuration, ILogger<AnagraficaService> logger)
        {
            _connectionString = configuration.GetConnectionString("AuthDb")!;
            _logger = logger;
        }

        public Anagrafica Create(Anagrafica anagrafica)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    _logger.LogInformation("Database connection opened.");
                    using (var command = new SqlCommand(CREATE_ANAGRAFICA_COMMAND, connection))
                    {
                        command.Parameters.AddWithValue("@Cognome", anagrafica.Cognome);
                        command.Parameters.AddWithValue("@Nome", anagrafica.Nome);
                        command.Parameters.AddWithValue("@Indirizzo", anagrafica.Indirizzo);
                        command.Parameters.AddWithValue("@Città", anagrafica.Città);
                        command.Parameters.AddWithValue("@CAP", anagrafica.CAP);
                        command.Parameters.AddWithValue("@Cod_Fisc", anagrafica.Cod_Fisc);

                        _logger.LogInformation("Executing SQL command: {Command}", CREATE_ANAGRAFICA_COMMAND);
                        anagrafica.IDAnagrafica = (int)command.ExecuteScalar();
                        _logger.LogInformation("Anagrafica created with ID: {IDAnagrafica}", anagrafica.IDAnagrafica);
                    }
                    return anagrafica;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Anagrafica");
                throw new Exception("Error creating Anagrafica: " + ex.Message);
            }
        }


        private const string GET_ALL_VERBALI_BY_PUNTI_DECURTATI_COMMAND = "SELECT a.IDAnagrafica, a.Nome, a.Cognome, SUM(v.DecurtamentoPunti) AS TotalePuntiDecurtati " +
           "FROM [dbo].[VERBALE] v " +
           "JOIN [dbo].[ANAGRAFICA] a ON v.IDAnagrafica = a.IDAnagrafica " +
           "GROUP BY a.IDAnagrafica, a.Nome, a.Cognome " +
           "ORDER BY TotalePuntiDecurtati DESC;";
        public List<PuntiDecurtati> GetAllTrasgressoreByPuntiDecurtati()
        {
            var result = new List<PuntiDecurtati>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(GET_ALL_VERBALI_BY_PUNTI_DECURTATI_COMMAND, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var verbalePuntiDecurtati = new PuntiDecurtati
                                {
                                    IDAnagrafica = reader.GetInt32(0),
                                    Nome = reader.GetString(1),
                                    Cognome = reader.GetString(2),
                                    TotalePuntiDecurtati = reader.GetInt32(3)
                                };
                                result.Add(verbalePuntiDecurtati);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving verbali by punti decurtati: " + ex.Message);
            }

            return result;
        }
    }
}
