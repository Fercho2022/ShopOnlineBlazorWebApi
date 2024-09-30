using Blazored.LocalStorage;
using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Services
{
    public class ManageCartItemsLocal_StorageService : IManageCartItemsLocalStorageService
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ILocalStorageService _localStorageService;
        private const string key = "CartItemCollection";

        public ManageCartItemsLocal_StorageService(IShoppingCartService shoppingCartService, ILocalStorageService localStorageService)
        {
            _shoppingCartService = shoppingCartService;
            _localStorageService = localStorageService;
        }

        public async Task<List<CartItemDto>> GetCollection()
        {
            /*return  await _localStorageService.GetItemAsync<List<CartItemDto>>(key) ?? await AddCollection()*/;
            var cartItems = await _localStorageService.GetItemAsync<List<CartItemDto>>(key);

            if (cartItems == null)
            {
                cartItems = (await AddCollection()).ToList(); // Convierte la colección a List<CartItemDto>
            }

            return cartItems;

        }

        public async Task RemoveCollection()
        {
           await _localStorageService.RemoveItemAsync(key);
        }

        public async Task SaveCollection(List<CartItemDto> cartItemDtos)
        {
            await _localStorageService.SetItemAsync(key, cartItemDtos);
        }

        public async Task<IEnumerable<CartItemDto>> AddCollection()
        {
            var shoppingCartCollection = await _shoppingCartService.GetItems(HardCoded.UserId);

            if (shoppingCartCollection != null)
            {
                await _localStorageService.SetItemAsync(key, shoppingCartCollection);
            }
            return shoppingCartCollection;
        }
    }
}
