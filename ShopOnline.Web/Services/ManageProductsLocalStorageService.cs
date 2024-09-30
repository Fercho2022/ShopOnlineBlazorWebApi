using Blazored.LocalStorage;
using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Services
{
    public class ManageProductsLocalStorageService : IManageProductsLocalStorageService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IProductService _productService;

        private const string key = "ProductCollection";

        public ManageProductsLocalStorageService(ILocalStorageService localStorageService, IProductService productService)
        {
            _localStorageService = localStorageService;
            _productService = productService;
        }

        public async Task<IEnumerable<ProductDto>> AddCollection()
        {
            var productCollection= await _productService.GetItems();
            
            if (productCollection != null)
            {
               await _localStorageService.SetItemAsync(key, productCollection);
            }
            return productCollection;
        }

        public async Task<IEnumerable<ProductDto>> GetCollection()
        {
            return await _localStorageService.GetItemAsync<IEnumerable<ProductDto>>(key) ?? await AddCollection();
        }

        public async Task RemoveCollection()
        {
            await _localStorageService.RemoveItemAsync(key);
        }
    }
}
