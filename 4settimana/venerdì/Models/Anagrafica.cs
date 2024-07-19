using System.ComponentModel.DataAnnotations;

namespace venerdì.Models
{
    public class Anagrafica
    {
        public int IDAnagrafica { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [StringLength(50)]
        public string Cognome { get; set; }

        [Required]
        [StringLength(100)]
        public string Indirizzo { get; set; }

        [Required]
        [StringLength(50)]
        public string Città { get; set; }

        [Required]
        [StringLength(5)]
        public string CAP { get; set; }

        [Required]
        [StringLength(16)]
        public string Cod_Fisc { get; set; }
    }
}
