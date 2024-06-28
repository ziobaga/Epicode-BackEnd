using esercizio4;

using esercizio4.Classi;
using System.ComponentModel.DataAnnotations;

namespace esercizio4.Models
{
    public class AcquistoDati
    {
        [Display(Name = "Nome")]
        public string name { get; set; }

        [Display(Name = "Cognome")]
        public string surname { get; set; }

        [Display(Name = "Biglietto")]
        public Biglietto tipoBiglietto { get; set; }

        [Display(Name = "Sala")]
        public List<Sala> sala { get; set; }

      
    }
}
