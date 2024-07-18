namespace ProgettoSettimanale.Models
{
    public class Spedizione
    {
        public int IdSpedizione { get; set; }
        public int? FK_ClienteAzienda { get; set; }
        public int? FK_ClientePrivato { get; set; }
        public int NumId { get; set; }
        public DateTime DataSpedizione { get; set; }
        public decimal Peso { get; set; }
        public string CittaDestinatario { get; set; }
        public string Indirizzo { get; set; }
        public string NomeDestinatario { get; set; }
        public decimal CostoSpedizione { get; set; }
        public DateTime DataConsegnaPrev { get; set; }

        public virtual ClienteAzienda ClienteAzienda { get; set; }
        public virtual ClientePrivato ClientePrivato { get; set; }
        public DateTime DataOraAggiornamento { get; set; }
    }
}
