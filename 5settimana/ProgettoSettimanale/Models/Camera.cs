using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimanale.Models
{
    public class Camera
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il numero della camera è obbligatorio.")]
        public int Numero { get; set; }

        public string? Descrizione { get; set; }

        [Required(ErrorMessage = "La tipologia della camera è obbligatoria.")]
        public string? Tipologia { get; set; }

    }
}