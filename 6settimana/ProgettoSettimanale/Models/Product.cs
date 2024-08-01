using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimanale.Models
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProduct { get; set; }

        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

        [Required]
        [Range(0, 1000)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }



        public byte[]? Image { get; set; }

        [Required]
        [Range(0, 60)]
        public int DeliveryTimeMin { get; set; }


        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}
