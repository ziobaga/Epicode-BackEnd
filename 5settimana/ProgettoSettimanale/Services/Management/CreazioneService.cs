﻿
using Project.Models;
using System.Data;
using System.Data.SqlClient;

namespace Project.Services.Management
{
    public class CreazioneService : CommonService, ICreazioneService
    {
        private const string CREA_PERSONA_COMMAND = "INSERT INTO [dbo].[Persone] " +
            "(Nome,Cognome,CF,Email,Telefono,Cellulare,Città,Provincia) " +
            "OUTPUT INSERTED.IdPersona " +
            "VALUES (@Nome, @Cognome, @CF,@Email,@Telefono,@Cellulare, @Città, @Provincia)";

        private const string CREA_CAMERA_COMMAND = "INSERT INTO [dbo].[Camere] " +
            "(NumeroCamera, Descrizione, Tipologia) " +
            "OUTPUT INSERTED.IdCamera " +
            "VALUES (@NumeroCamera, @Descrizione, @Tipologia)";
        
        private const string CREA_PRENOTAZIONE_COMMAND = "INSERT INTO [dbo].[Prenotazioni] " +
            "(DataPrenotazione, NumProgressivo, Anno, SoggiornoDal, SoggiornoAl, Caparra, Tariffa, TipoPensione, IdPersona, IdCamera) " +
            "OUTPUT INSERTED.IdPrenotazione " +
            "VALUES (@DataPrenotazione, @NumProgressivo, @Anno, @SoggiornoDal, @SoggiornoAl, @Caparra, @Tariffa, @TipoPensione, @IdPersona, @IdCamera)";

        private const string GET_NUM_PROGRESSIVO_COMMAND = "GetNextNumProgressivo";

        private readonly ILogger<CreazioneService> _logger;

        public CreazioneService(IConfiguration configuration, ILogger<CreazioneService> logger) : base(configuration.GetConnectionString("AuthDb"))
        {
            _logger = logger;
        }


        public Persona CreaPersona(Persona persona)
        {
            try
            {
                var personaId = ExecuteScalar<int>(CREA_PERSONA_COMMAND, command =>
                {
                    command.Parameters.AddWithValue("@Nome", persona.Nome);
                    command.Parameters.AddWithValue("@Cognome", persona.Cognome);
                    command.Parameters.AddWithValue("@CF", persona.CF);
                    command.Parameters.AddWithValue("@Email", persona.Email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Telefono", persona.Telefono ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Cellulare", persona.Cellulare ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Città", persona.Città ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Provincia", persona.Provincia ?? (object)DBNull.Value);
                });

                return new Persona
                {
                    IdPersona = personaId,
                    Nome = persona.Nome,
                    Cognome = persona.Cognome,
                    CF = persona.CF,
                    Email = persona.Email,
                    Telefono = persona.Telefono,
                    Cellulare = persona.Cellulare,
                    Città = persona.Città,
                    Provincia = persona.Provincia
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Si è verificato un errore inatteso durante la creazione della persona.");
                throw new Exception("Si è verificato un errore inatteso. Riprova più tardi.");
            }
        }

        public Camera CreaCamera(Camera camera)
        {
            try
            {
                var cameraId = ExecuteScalar<int>(CREA_CAMERA_COMMAND, command =>
                {
                    command.Parameters.AddWithValue("@NumeroCamera", camera.NumeroCamera);
                    command.Parameters.AddWithValue("@Descrizione", camera.Descrizione ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Tipologia", camera.Tipologia ?? (object)DBNull.Value);
                });

                return new Camera
                {
                    IdCamera = cameraId,
                    NumeroCamera = camera.NumeroCamera,
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
        public Prenotazione CreaPrenotazione(Prenotazione prenotazione)
        {
            try
            {
                prenotazione.DataPrenotazione = DateTime.Now;

                prenotazione.Anno = prenotazione.SoggiornoDal.Year;

                int nextNumProgressivo;
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(GET_NUM_PROGRESSIVO_COMMAND, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Anno", prenotazione.Anno);

                        var outputParam = new SqlParameter("@NextNumProgressivo", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParam);

                        command.ExecuteNonQuery();
                        nextNumProgressivo = (int)outputParam.Value;
                    }
                }

                prenotazione.NumProgressivo = nextNumProgressivo;

                var prenotazioneId = ExecuteScalar<int>(CREA_PRENOTAZIONE_COMMAND, command =>
                {
                    command.Parameters.AddWithValue("@DataPrenotazione", prenotazione.DataPrenotazione);
                    command.Parameters.AddWithValue("@NumProgressivo", prenotazione.NumProgressivo);
                    command.Parameters.AddWithValue("@Anno", prenotazione.Anno);
                    command.Parameters.AddWithValue("@SoggiornoDal", prenotazione.SoggiornoDal);
                    command.Parameters.AddWithValue("@SoggiornoAl", prenotazione.SoggiornoAl);
                    command.Parameters.AddWithValue("@Caparra", prenotazione.Caparra);
                    command.Parameters.AddWithValue("@Tariffa", prenotazione.Tariffa);

                    command.Parameters.AddWithValue("@TipoPensione", prenotazione.TipoPensione.ToString() ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IdPersona", prenotazione.IdPersona);
                    command.Parameters.AddWithValue("@IdCamera", prenotazione.IdCamera);
                });

                return new Prenotazione
                {
                    IdPrenotazione = prenotazioneId,
                    DataPrenotazione = prenotazione.DataPrenotazione,
                    NumProgressivo = prenotazione.NumProgressivo,
                    Anno = prenotazione.Anno,
                    SoggiornoDal = prenotazione.SoggiornoDal,
                    SoggiornoAl = prenotazione.SoggiornoAl,
                    Caparra = prenotazione.Caparra,
                    Tariffa = prenotazione.Tariffa,
                    TipoPensione = prenotazione.TipoPensione,
                    IdPersona = prenotazione.IdPersona,
                    IdCamera = prenotazione.IdCamera
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
