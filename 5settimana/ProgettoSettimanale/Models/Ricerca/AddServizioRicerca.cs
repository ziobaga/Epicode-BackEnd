namespace ProgettoSettimanale.Models.Ricerca
{
    public class AddServizioRicerca
    {
        public int PrenotazioneId { get; set; }
        public int ServizioId { get; set; }
        public DateTime Data { get; set; }
        public int Quantita { get; set; }
        public decimal Prezzo { get; set; }
    }
}
