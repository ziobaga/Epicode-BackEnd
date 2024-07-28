﻿namespace Project.Models.ViewModels
{
    public class CheckoutVM
    {
        public int IdPrenotazione { get; set; }
        public int NumeroCamera { get; set; }
        public DateTime SoggiornoDal { get; set; }
        public DateTime SoggiornoAl { get; set; }
        public decimal Tariffa { get; set; }
        public decimal Caparra { get; set; }
        public decimal ImportoDaSaldare { get; set; }

        public List<ServizioAggViewModel> ServiziAgg { get; set; } = new List<ServizioAggViewModel>();
    }

    public class ServizioAggViewModel
    {
        public string ServizioAgg { get; set; }
        public DateTime? DataServizio { get; set; }
        public int? Quantita { get; set; }
        public decimal? Prezzo { get; set; }
    }
    public class StanzaViewModel
    {
        public int NumeroCamera { get; set; }
        public DateTime SoggiornoDal { get; set; }
        public DateTime SoggiornoAl { get; set; }
        public decimal Tariffa { get; set; }
        public decimal Caparra { get; set; }
    }

}
