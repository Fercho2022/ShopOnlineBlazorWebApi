using ShopOnline.Api.Entities;

namespace ShopOnline.Api.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetItems();

        Task<IEnumerable<ProductCategory>> GetCategories();

        Task<Product> GetItem(int id);

        Task<ProductCategory> GetCategoryById(int id);

        // Nuevo método para obtener productos por categoría
        Task<IEnumerable<Product>> GetItemsByCategory(int categoryId);

       
    }
}
