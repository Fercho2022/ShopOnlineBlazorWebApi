using Microsoft.EntityFrameworkCore;
using ShopOnline.Api.Data;
using ShopOnline.Api.Entities;
using ShopOnline.Api.Interfaces;

namespace ShopOnline.Api.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductCategory>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }
         
        public async Task<ProductCategory> GetCategoryById(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            return await _context.Products
                .Include(p => p.ProductCategory)   // Cargar la categoría junto con los productos
                .ToListAsync();
        }

       public async Task<Product> GetItem(int id)
        {
            return await _context.Products
                .Include(p=>p.ProductCategory) // Incluir la categoría relacionada
                .SingleOrDefaultAsync(p=>p.Id == id);   // Buscar producto con el Id específico
        }

       

        public async Task<IEnumerable<Product>> GetItemsByCategory(int categoryId)
        {
            var productsByCategory= await _context.Products
                .Include(p => p.ProductCategory)       // Cargar la categoría junto con los productos
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();

            return productsByCategory;
        }
    }
}

