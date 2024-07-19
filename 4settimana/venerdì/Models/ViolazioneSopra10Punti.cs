namespace venerdì.Models
{
    public class ViolazioneSopra10Punti
    {
        public decimal Importo { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public DateTime DataViolazione { get; set; }
        public int DecurtamentoPunti { get; set; }

    }
}
