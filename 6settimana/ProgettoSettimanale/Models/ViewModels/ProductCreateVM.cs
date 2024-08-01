namespace ProgettoSettimanale.Models.ViewModels
{
    public class ProductCreateVM
    {
        public Product Product { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public List<int> SelectedIngredientIds { get; set; } = new List<int>();

        public IFormFile? Image { get; set; }
    }
}
