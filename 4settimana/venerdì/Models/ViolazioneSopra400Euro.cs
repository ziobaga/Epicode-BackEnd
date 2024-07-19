namespace venerdì.Models
{
    public class ViolazioneSopra400Euro
    {

        public decimal Importo { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public DateTime DataViolazione { get; set; }
    }
}
