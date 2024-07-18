using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimanale.Models
{
    public class EmailViewModel
    {

        [Display(Name = "La tua mail")]
        public string ToEmail { get; set; }

        [Display(Name = "Oggetto")]
        public string Subject { get; set; }

        [Display(Name = "Il tuo messaggio")]
        public string Message { get; set; }
    }
}
