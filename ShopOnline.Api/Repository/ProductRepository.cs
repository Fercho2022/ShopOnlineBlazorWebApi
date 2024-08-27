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

        public async Task<IEnumerable<ProductCategory>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<ProductCategory> GetCategoryById(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            return await _context.Products.ToListAsync();
        }

       public async Task<Product> GetItemById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

    }
}

