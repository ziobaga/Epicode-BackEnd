namespace ProgettoSettimanale.Models
{
    public class Prenotazione
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public int CameraId { get; set; }
        public Camera Camera { get; set; }
        public DateTime DataPrenotazione { get; set; }
        public int NumeroProgressivo { get; set; }
        public int Anno { get; set; }
        public DateTime Dal { get; set; }
        public DateTime Al { get; set; }
        public decimal CaparraConfirmatoria { get; set; }
        public decimal Tariffa { get; set; }
        public string DettagliSoggiorno { get; set; }
        
    }
}