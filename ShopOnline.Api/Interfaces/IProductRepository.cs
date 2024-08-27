using ShopOnline.Api.Entities;

namespace ShopOnline.Api.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetItems();

        Task<IEnumerable<ProductCategory>> GetAllCategories();

        Task<Product> GetItemById(int id);

        Task<ProductCategory> GetCategoryById(int id);
    }
}
