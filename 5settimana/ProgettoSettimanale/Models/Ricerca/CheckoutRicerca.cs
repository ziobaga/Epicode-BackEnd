namespace ProgettoSettimanale.Models.Ricerca
{
    public class CheckoutRicerca
    {
        public int PrenotazioneId { get; set; }
        public int Numero {  get; set; }
        public DateTime Dal {  get; set; }
        public DateTime Al {  get; set; }
        public decimal Tariffa { get; set; }
        public decimal CaparraConfirmatoria { get; set; }
        public decimal ImportoDaSaldare { get; set; }

        public List<AddServizioRicerca> ServiziAgg { get; set; } = new List<AddServizioRicerca>();

    }

    public class ServizioAggRicerca
    {
        public string Nome { get; set; }
        public DateTime? Data { get; set; }
        public int? Quantita { get; set; }
        public decimal? Prezzo { get; set; }
    }

    public class CameraRicerca
    {

        public int Numero { get; set; }
        public DateTime Dal { get; set; }
        public DateTime Al { get; set; }
        public decimal Tariffa { get; set; }
        public decimal CaparraConfirmatoria { get; set; }
    }
}
