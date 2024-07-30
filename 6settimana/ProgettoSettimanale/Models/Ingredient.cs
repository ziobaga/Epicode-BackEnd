using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ProgettoSettimanale.Models
{
    public class Ingredient
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //todo:da verificare [JsonIgnore]
        public int IdIngredient { get; set; }

        [Required]
        [StringLength(50)]
        public required string Name { get; set; }


        // Riferimenti EF
        [JsonIgnore]
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
