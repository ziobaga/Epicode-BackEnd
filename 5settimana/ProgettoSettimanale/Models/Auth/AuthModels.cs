using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimanale.Models.Auth
{
    public class AuthModels
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
