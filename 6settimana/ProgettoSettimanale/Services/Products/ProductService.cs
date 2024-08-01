using ProgettoSettimanale.Context;
using ProgettoSettimanale.Models;
using ProgettoSettimanale.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ProgettoSettimanale.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly DataContext _dataContext;

        public ProductService(DataContext context)
        {
            _dataContext = context;
        }

        public async Task<List<Ingredient>> GetAllIngredientsAsync()
        {
            return await _dataContext.Ingredients.ToListAsync();
        }
        public async Task<Product> CreateProductIngredientsAsync(ProductCreateVM viewModel)
        {
            var product = viewModel.Product;

            if (viewModel.Image != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await viewModel.Image.CopyToAsync(memoryStream);
                    product.Image = memoryStream.ToArray();
                }
            }

            product.Ingredients = await _dataContext.Ingredients
                .Where(i => viewModel.SelectedIngredientIds.Contains(i.IdIngredient))
                .ToListAsync();

            await _dataContext.Products.AddAsync(product);
            await _dataContext.SaveChangesAsync();

            return product;
        }


        public async Task<List<Product>> CreateProductsIngredientsAsync()
        {
            return await _dataContext.Products
                .Include(p => p.Ingredients)
                .ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            var productById = await _dataContext.Products.FirstOrDefaultAsync(p => p.IdProduct == productId);
            return productById;
        }


        public async Task<List<Product>> GetAllProductsIngredientsAsync()
        {
            return await _dataContext.Products
                .Include(p => p.Ingredients)
                .ToListAsync();
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _dataContext.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }

            _dataContext.Products.Remove(product);
            await _dataContext.SaveChangesAsync();
            return true;
        }
    }
}
