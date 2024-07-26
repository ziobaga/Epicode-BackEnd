﻿using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimanale.Models.Ricerca
{
    public class RicercaPrenotazioneByCFRicerca
    {
        [Required(ErrorMessage = "Il Codice Fiscale è obbligatorio.")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Il Codice Fiscale deve contenere esattamente 16 caratteri.")]
        [RegularExpression(@"^[^\s]{16}$", ErrorMessage = "Il Codice Fiscale deve essere di 16 caratteri e non deve contenere spazi.")]
        public string codiceFiscale { get; set; }
    }
}
