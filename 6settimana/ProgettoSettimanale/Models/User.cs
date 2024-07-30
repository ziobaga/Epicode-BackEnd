using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimanale.Models
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUser { get; set; }

        [Required]
        [StringLength(20)]
        public required string Username { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50)]
        public required string Email { get; set; }

        [Required]
        [StringLength(50)]
        public required string Password { get; set; }

        // Riferimenti EF
        public List<Role> Roles { get; set; } = new List<Role>();
    }
}
