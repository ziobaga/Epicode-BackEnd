namespace ProgettoSettimanale.Models
{
    public class StatoSpedizione
    {
        public int IdStatoSpedizione { get; set; }
        public string Stato { get; set; }
        public string Luogo { get; set; }
        public string Descrizione { get; set; }
        public DateTime DataOraAggiornamento { get; set; }
        public int FK_IdSpedizione { get; set; }

        public virtual Spedizione Spedizione { get; set; }
    }
}
