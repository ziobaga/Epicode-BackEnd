using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimanale.Models.Ricerca
{
    public class RicercaPrenotazioneByDettagliSoggiornoRicerca
    {
        [Required(ErrorMessage = "Il tipo di pensione è richiesto.")]
        [RegularExpression(@"^(Solo Pernottamento|Pensione Completa|Mezza Pensione)$", ErrorMessage = "Tipo di pensione non valido.")]
        public string DettagliSoggiorno { get; set; }
    }
}
