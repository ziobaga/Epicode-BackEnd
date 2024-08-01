using ProgettoSettimanale.Models;
using ProgettoSettimanale.Models.ViewModels;

namespace ProgettoSettimanale.Services.Products
{
    public interface IProductService
    {
        Task<List<Ingredient>> GetAllIngredientsAsync();
        Task<Product> CreateProductIngredientsAsync(ProductCreateVM viewModel);
        Task<List<Product>> GetAllProductsIngredientsAsync();
        Task<Product> GetProductByIdAsync(int IdProduct);
        Task<bool> DeleteProductAsync(int id);
    }
}
