using Microsoft.Data.SqlClient;
using venerdì.Models;

namespace venerdì.Services
{
    public class VerbaleService : IVerbaleService
    {
        private readonly string _connectionString;

        public VerbaleService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AuthDb");
        }


        private const string CREATE_VERBALE_COMMAND = "INSERT INTO [dbo].[VERBALE] " +
            "(DataViolazione, IndirizzoViolazione, Nominativo_Agente, DataTrascrizioneVerbale, Importo, DecurtamentoPunti, IDAnagrafica, IDViolazione) " +
            "OUTPUT INSERTED.IDVerbale " +
            "VALUES (@DataViolazione, @IndirizzoViolazione, @Nominativo_Agente, @DataTrascrizioneVerbale, @Importo, @DecurtamentoPunti, @IDAnagrafica, @IDViolazione)";
        public Verbale Create(Verbale verbale)
        {
            try
            {
                verbale.DataTrascrizioneVerbale = DateTime.Now;

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(CREATE_VERBALE_COMMAND, connection))
                    {
                        command.Parameters.AddWithValue("@DataViolazione", verbale.DataViolazione);
                        command.Parameters.AddWithValue("@IndirizzoViolazione", verbale.IndirizzoViolazione);
                        command.Parameters.AddWithValue("@Nominativo_Agente", verbale.Nominativo_Agente);
                        command.Parameters.AddWithValue("@DataTrascrizioneVerbale", verbale.DataTrascrizioneVerbale);
                        command.Parameters.AddWithValue("@Importo", verbale.Importo);
                        command.Parameters.AddWithValue("@DecurtamentoPunti", verbale.DecurtamentoPunti);
                        command.Parameters.AddWithValue("@IDAnagrafica", verbale.IDAnagrafica);
                        command.Parameters.AddWithValue("@IDViolazione", verbale.IDViolazione);

                        verbale.IDVerbale = (int)command.ExecuteScalar();
                    }
                    return verbale;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating Verbale: " + ex.Message);
            }
        }


        private const string GET_ALL_VERBALI_BY_TRASGRESSORE_COMMAND = "SELECT a.IDAnagrafica, a.Nome, a.Cognome, COUNT(v.IDVerbale) AS TotaleVerbali " +
            "FROM [dbo].[VERBALE] v " +
            "JOIN [dbo].[ANAGRAFICA] a ON v.IDAnagrafica = a.IDAnagrafica " +
            "GROUP BY a.IDAnagrafica, a.Nome, a.Cognome " +
            "ORDER BY TotaleVerbali DESC;";

        public List<VerbaliTrasgressori> GetAllVerbaliByTrasgressore()
        {
            var result = new List<VerbaliTrasgressori>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(GET_ALL_VERBALI_BY_TRASGRESSORE_COMMAND, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var verbaleByTrasgressore = new VerbaliTrasgressori
                                {
                                    IDAnagrafica = reader.GetInt32(0),
                                    Nome = reader.GetString(1),
                                    Cognome = reader.GetString(2),
                                    TotaleVerbali = reader.GetInt32(3)
                                };
                                result.Add(verbaleByTrasgressore);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving verbali by trasgressore: " + ex.Message);
            }

            return result;
        }



    }
}
